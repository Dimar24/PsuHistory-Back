using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Histories;
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
    public interface IAttachmentFormValidation : IBaseValidation<Guid, AttachmentForm>
    { }

    public class AttachmentFormValidation : IAttachmentFormValidation
    {
        private ValidationModel<AttachmentForm> validation;
        private readonly IBaseService<Guid, AttachmentForm> dataAttachmentForm;
        private readonly IBaseService<Guid, Form> dataForm;

        public AttachmentFormValidation(IBaseService<Guid, AttachmentForm> dataAttachmentForm, IBaseService<Guid, Form> dataForm)
        {
            this.dataAttachmentForm = dataAttachmentForm;
            this.dataForm = dataForm;
            validation = new ValidationModel<AttachmentForm>();
        }

        public async Task<ValidationModel<AttachmentForm>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataAttachmentForm.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(AttachmentForm), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentForm>> InsertValidationAsync(AttachmentForm newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if ((await dataForm.GetAsync(newEntity.FormId, cancellationToken)) is null)
                {
                    validation.Errors.Add(nameof(newEntity.FormId), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.File is null)
                {
                    validation.Errors.Add(nameof(newEntity.File), BaseValidation.FieldNotCanBeNull);
                }
            }
            else
            {
                validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentForm>> UpdateValidationAsync(AttachmentForm newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if ((await dataAttachmentForm.GetAsync(newEntity.Id, cancellationToken)) is null)
                {
                    validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotExistById);
                }

                if ((await dataForm.GetAsync(newEntity.FormId, cancellationToken)) is null)
                {
                    validation.Errors.Add(nameof(newEntity.FormId), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.File is null)
                {
                    validation.Errors.Add(nameof(newEntity.File), BaseValidation.FieldNotCanBeNull);
                }
            }
            else
            {
                validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<AttachmentForm>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataAttachmentForm.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(AttachmentForm), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
