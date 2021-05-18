using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.BusinessServices
{
    public interface IAttachmentBurialBusinessService : IBaseBusinessService<Guid, AttachmentBurial>
    { }

    class AttachmentBurialBusinessService : IAttachmentBurialBusinessService
    {
        private readonly IBaseService<Guid, AttachmentBurial> dataAttachmentBurial;
        private readonly IBaseValidation<Guid, AttachmentBurial> attachmentBurialValidation;
        private readonly string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public AttachmentBurialBusinessService(
            IBaseService<Guid, AttachmentBurial> dataAttachmentBurial,
            IBaseValidation<Guid, AttachmentBurial> attachmentBurialValidation)
        {
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

            newEntity = await SaveFile(newEntity);

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

            await dataAttachmentBurial.DeleteAsync(id, cancellationToken);

            return validation;
        }

        private async Task<AttachmentBurial> SaveFile(AttachmentBurial entity)
        {
            string path = "/Files/" + entity.BurialId.ToString();

            entity.FilePath = path;
            entity.FileName = Guid.NewGuid().ToString();
            //entity.FileType = entity.Fi;

            using (var fileStream = new FileStream(applicationPath + path, FileMode.Create))
            {
                await entity.File.CopyToAsync(fileStream);
            }

            return entity;
        }

        private async Task<AttachmentBurial> DeleteFile(AttachmentBurial entity)
        {
            string path = "/Files/" + entity.BurialId.ToString();

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
