using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (await dataAttachmentBurial.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(AttachmentBurial), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> InsertValidationAsync(AttachmentBurial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(AttachmentBurial.Burial), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.File is null)
                {
                    validation.Errors.Add(nameof(AttachmentBurial.File), BaseValidation.FieldNotCanBeNull);
                }
            }
            else
            {
                validation.Errors.Add(nameof(AttachmentBurial), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> UpdateValidationAsync(AttachmentBurial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataAttachmentBurial.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(AttachmentBurial), BaseValidation.ObjectNotExistById);
                }

                if (await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(AttachmentBurial.Burial), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.File is null)
                {
                    validation.Errors.Add(nameof(AttachmentBurial.File), BaseValidation.FieldNotCanBeNull);
                }
            }
            else
            {
                validation.Errors.Add(nameof(AttachmentBurial), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentBurial>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (await dataAttachmentBurial.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(AttachmentBurial), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
