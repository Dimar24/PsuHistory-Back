using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface IAttachmentBurialService : IBaseService<Guid, AttachmentBurial>
    { }

    public class AttachmentBurialService : IAttachmentBurialService
    {
        private readonly FileHelper fileHelper;
        private readonly IBaseRepository<Guid, AttachmentBurial> dataAttachmentBurial;
        private readonly IBaseValidation<Guid, AttachmentBurial> attachmentBurialValidation;

        public AttachmentBurialService(
            FileHelper fileHelper,
            IBaseRepository<Guid, AttachmentBurial> dataAttachmentBurial,
            IBaseValidation<Guid, AttachmentBurial> attachmentBurialValidation)
        {
            this.fileHelper = fileHelper;
            this.dataAttachmentBurial = dataAttachmentBurial;
            this.attachmentBurialValidation = attachmentBurialValidation;
        }

        public async Task<ValidationModel<AttachmentBurial>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentBurialValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataAttachmentBurial.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<AttachmentBurial>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<AttachmentBurial>>();

            validation.Result = await dataAttachmentBurial.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> InsertAsync(AttachmentBurial newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentBurialValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var fileData = await fileHelper.SaveFile(newEntity.File);

            newEntity.FilePath = fileData.FilePath;
            newEntity.FileName = fileData.FileName;
            newEntity.FileType = fileData.FileType;
            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataAttachmentBurial.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> UpdateAsync(AttachmentBurial newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentBurialValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataAttachmentBurial.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentBurialValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var entity = await dataAttachmentBurial.GetAsync(id, cancellationToken);

            fileHelper.DeleteFile(entity.FilePath + entity.FileName + "." + entity.FileType);

            await dataAttachmentBurial.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
