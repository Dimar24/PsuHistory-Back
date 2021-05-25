using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Models;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IAttachmentBurialValidation : IBaseValidation<Guid, AttachmentBurial>
    { }

    public class AttachmentBurialValidation : IAttachmentBurialValidation
    {
        private ValidationModel<AttachmentBurial> validation;
        private readonly IBaseRepository<Guid, AttachmentBurial> dataAttachmentBurial;
        private readonly IBaseRepository<Guid, Burial> dataBurial;

        public AttachmentBurialValidation(IBaseRepository<Guid, AttachmentBurial> dataAttachmentBurial, IBaseRepository<Guid, Burial> dataBurial)
        {
            this.dataAttachmentBurial = dataAttachmentBurial;
            this.dataBurial = dataBurial;
            validation = new ValidationModel<AttachmentBurial>();
        }

        public async Task<ValidationModel<AttachmentBurial>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataAttachmentBurial.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(AttachmentBurial), 
                    string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial), id));
            }

            return null;
        }

        public async Task<ValidationModel<AttachmentBurial>> InsertValidationAsync(AttachmentBurial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (newEntity.File is null)
                {
                    validation.Errors.Add(nameof(AttachmentBurial.File),
                        string.Format(BaseValidation.FileNotCanBeNull));
                }
            }
            else
            {
                validation.Errors.Add(nameof(AttachmentBurial),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(AttachmentBurial)));
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> UpdateValidationAsync(AttachmentBurial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataAttachmentBurial.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(AttachmentBurial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial), newEntity.Id));    
                }

                if (!await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(AttachmentBurial.Burial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial.Burial), newEntity.BurialId));
                }

                if (newEntity.File is null)
                {
                    validation.Errors.Add(nameof(AttachmentBurial.File),
                        string.Format(BaseValidation.FileNotCanBeNull));
                }
            }
            else
            {
                validation.Errors.Add(nameof(AttachmentBurial),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(AttachmentBurial)));
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataAttachmentBurial.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(AttachmentBurial),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial), id));
            }

            return validation;
        }
    }
}
