using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
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
        private readonly IBaseService<Guid, AttachmentBurial> dataAttachmentBurial;
        private readonly IBaseService<Guid, Burial> dataBurial;

        public AttachmentBurialValidation(IBaseService<Guid, AttachmentBurial> dataAttachmentBurial, IBaseService<Guid, Burial> dataBurial)
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

            return validation;
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
