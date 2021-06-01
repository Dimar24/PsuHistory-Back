using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface IFormService : IBaseService<Guid, Form>
    { }

    public class FormService : IFormService
    {
        private readonly FileHelper fileHelper;
        private readonly IBaseRepository<Guid, Form> dataForm;
        private readonly IBaseRepository<Guid, AttachmentForm> dataAttachmentForm;
        private readonly IBaseValidation<Guid, Form> formValidation;

        public FormService(
            FileHelper fileHelper,
            IBaseRepository<Guid, Form> dataForm,
            IBaseRepository<Guid, AttachmentForm> dataAttachmentForm,
            IBaseValidation<Guid, Form> formValidation)
        {
            this.fileHelper = fileHelper;
            this.dataForm = dataForm;
            this.dataAttachmentForm = dataAttachmentForm;
            this.formValidation = formValidation;
        }

        public async Task<ValidationModel<Form>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await formValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataForm.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<Form>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<Form>>();

            validation.Result = await dataForm.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<Form>> InsertAsync(Form newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await formValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }


            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataForm.InsertAsync(newEntity, cancellationToken);

            if(newEntity.Files is not null && !newEntity.Files.Count().Equals(0))
            {
                var fileDatas = await fileHelper.SaveFileRange(newEntity.Files);

                foreach(var file in fileDatas)
                {
                    var attachmentForm = new AttachmentForm()
                    {
                        FilePath = file.FilePath,
                        FileName = file.FileName,
                        FileType = file.FileType,
                        FormId = validation.Result.Id
                    };

                    await dataAttachmentForm.InsertAsync(attachmentForm, cancellationToken);
                }
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> UpdateAsync(Form newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await formValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataForm.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataForm.UpdateAsync(newEntity, cancellationToken);

            if (newEntity.Files is not null && !newEntity.Files.Count().Equals(0))
            {
                var fileDatas = await fileHelper.SaveFileRange(newEntity.Files);

                foreach (var file in fileDatas)
                {
                    var attachmentForm = new AttachmentForm()
                    {
                        FilePath = file.FilePath,
                        FileName = file.FileName,
                        FileType = file.FileType,
                        FormId = validation.Result.Id
                    };

                    await dataAttachmentForm.InsertAsync(attachmentForm, cancellationToken);
                }
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await formValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataForm.DeleteAsync(id, cancellationToken);

            //var allFiles = await dataAttachmentForm.GetAllAsync();
            //
            //foreach (var file in allFiles.Where(af => af.FormId == id).ToList())
            //{
            //    await dataAttachmentForm.DeleteAsync(file.Id, cancellationToken);
            //    fileHelper.DeleteFile(file.FilePath + file.FileName + "." + file.FileType);
            //}

            return validation;
        }
    }
}
