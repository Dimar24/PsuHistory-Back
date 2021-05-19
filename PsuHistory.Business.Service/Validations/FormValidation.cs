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
    public interface IFormValidation : IBaseValidation<Guid, Form>
    { }

    public class FormValidation : IFormValidation
    {
        private ValidationModel<Form> validation;
        private readonly IBaseService<Guid, Form> dataForm;

        public FormValidation(IBaseService<Guid, Form> dataForm)
        {
            this.dataForm = dataForm;
            validation = new ValidationModel<Form>();
        }

        public async Task<ValidationModel<Form>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataForm.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(Form), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> InsertValidationAsync(Form newEntity, CancellationToken cancellationToken = default)
        {
            if (await dataForm.ExistAsync(newEntity, cancellationToken))
            {
                validation.Errors.Add(nameof(Form), BaseValidation.ObjectExistWithThisData);
            }

            if (newEntity.LastName is null)
            {
                validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldNotCanBeNull);
            }
            else
            {
                if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldInvalidLength);
                }
            }

            if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
            {
                validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldInvalidLength);
            }

            if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
            {
                validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldInvalidLength);
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> UpdateValidationAsync(Form newEntity, CancellationToken cancellationToken = default)
        {
            if ((await dataForm.GetAsync(newEntity.Id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(Form), BaseValidation.ObjectNotExistById);
            }

            if (await dataForm.ExistAsync(newEntity, cancellationToken))
            {
                validation.Errors.Add(nameof(Form), BaseValidation.ObjectExistWithThisData);
            }

            if (newEntity.LastName is null)
            {
                validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldNotCanBeNull);
            }
            else
            {
                if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldInvalidLength);
                }
            }

            if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
            {
                validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldInvalidLength);
            }

            if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
            {
                validation.Errors.Add(nameof(newEntity.LastName), BaseValidation.FieldInvalidLength);
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataForm.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(Form), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
