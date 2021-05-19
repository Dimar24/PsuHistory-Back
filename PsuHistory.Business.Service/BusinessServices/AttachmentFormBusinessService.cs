using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.BusinessServices
{
    public interface IAttachmentFormBusinessService : IBaseBusinessService<Guid, AttachmentForm>
    { }

    class AttachmentFormBusinessService : IAttachmentFormBusinessService
    {
        private readonly IBaseService<Guid, AttachmentForm> dataAttachmentForm;
        private readonly IBaseValidation<Guid, AttachmentForm> attachmentFormValidation;
        private readonly string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public AttachmentFormBusinessService(
            IBaseService<Guid, AttachmentForm> dataAttachmentForm,
            IBaseValidation<Guid, AttachmentForm> attachmentFormValidation)
        {
            this.dataAttachmentForm = dataAttachmentForm;
            this.attachmentFormValidation = attachmentFormValidation;
        }

        public async Task<ValidationModel<AttachmentForm>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentFormValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataAttachmentForm.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<AttachmentForm>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<AttachmentForm>>();

            validation.Result = await dataAttachmentForm.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<AttachmentForm>> InsertAsync(AttachmentForm newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentFormValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity = await SaveFile(newEntity);

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataAttachmentForm.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<AttachmentForm>> UpdateAsync(AttachmentForm newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentFormValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataAttachmentForm.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<AttachmentForm>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await attachmentFormValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataAttachmentForm.DeleteAsync(id, cancellationToken);

            return validation;
        }

        private async Task<AttachmentForm> SaveFile(AttachmentForm entity)
        {
            string path = "/Files/" + entity.FormId.ToString();

            entity.FilePath = path;
            entity.FileName = Guid.NewGuid().ToString();
            //entity.FileType = entity.Fi;

            using (var fileStream = new FileStream(applicationPath + path, FileMode.Create))
            {
                await entity.File.CopyToAsync(fileStream);
            }

            return entity;
        }

        private async Task<AttachmentForm> DeleteFile(AttachmentForm entity)
        {
            string path = "/Files/" + entity.FormId.ToString();

            entity.FilePath = path;
            entity.FileName = Guid.NewGuid().ToString();
            //entity.FileType = entity.Fi;

            using (var fileStream = new FileStream(applicationPath + path, FileMode.Create))
            {
                await entity.File.CopyToAsync(fileStream);
            }

            return entity;
        }
    }
}
