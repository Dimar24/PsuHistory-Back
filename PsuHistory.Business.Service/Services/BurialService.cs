using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface IBurialService : IBaseService<Guid, Burial>
    { }

    class BurialService : IBurialService
    {
        private readonly FileHelper fileHelper;
        private readonly IBaseRepository<Guid, Burial> dataBurial;
        private readonly IBaseRepository<Guid, AttachmentBurial> dataAttachmentBurial;
        private readonly IBaseValidation<Guid, Burial> burilValidation;

        public BurialService(
            FileHelper fileHelper,
            IBaseRepository<Guid, Burial> dataBurial,
            IBaseRepository<Guid, AttachmentBurial> dataAttachmentBurial,
            IBaseValidation<Guid, Burial> burilValidation)
        {
            this.fileHelper = fileHelper;
            this.dataBurial = dataBurial;
            this.dataAttachmentBurial = dataAttachmentBurial;
            this.burilValidation = burilValidation;
        }

        public async Task<ValidationModel<Burial>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await burilValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataBurial.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<Burial>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<Burial>>();

            validation.Result = await dataBurial.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<Burial>> InsertAsync(Burial newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await burilValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataBurial.InsertAsync(newEntity, cancellationToken);

            if (newEntity.Files is not null && !newEntity.Files.Count().Equals(0))
            {
                var fileDatas = await fileHelper.SaveFileRange(newEntity.Files);

                foreach (var file in fileDatas)
                {
                    var attachmentForm = new AttachmentBurial()
                    {
                        FilePath = file.FilePath,
                        FileName = file.FileName,
                        FileType = file.FileType,
                        BurialId = validation.Result.Id
                    };

                    await dataAttachmentBurial.InsertAsync(attachmentForm, cancellationToken);
                }
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> UpdateAsync(Burial newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await burilValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataBurial.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataBurial.UpdateAsync(newEntity, cancellationToken);

            if (newEntity.Files is not null && !newEntity.Files.Count().Equals(0))
            {
                var fileDatas = await fileHelper.SaveFileRange(newEntity.Files);

                foreach (var file in fileDatas)
                {
                    var attachmentBurial = new AttachmentBurial()
                    {
                        FilePath = file.FilePath,
                        FileName = file.FileName,
                        FileType = file.FileType,
                        BurialId = validation.Result.Id
                    };

                    await dataAttachmentBurial.InsertAsync(attachmentBurial, cancellationToken);
                }
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await burilValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataBurial.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
