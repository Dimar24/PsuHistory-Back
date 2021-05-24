using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Models;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IFormValidation : IBaseValidation<Guid, Form>
    { }

    public class FormValidation : IFormValidation
    {
        private ValidationModel<Form> validation;
        private readonly IBaseRepository<Guid, Form> dataForm;

        public FormValidation(IBaseRepository<Guid, Form> dataForm)
        {
            this.dataForm = dataForm;
            validation = new ValidationModel<Form>();
        }

        public async Task<ValidationModel<Form>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataForm.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Form),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(Form), id));
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> InsertValidationAsync(Form newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataForm.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Form),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(Form)));
                }

                if (newEntity.LastName is null)
                {
                    validation.Errors.Add(nameof(Form.LastName),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Form.LastName)));
                }
                else if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(Form.LastName),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Form.LastName), 3, 128));
                }

                if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
                {
                    validation.Errors.Add(nameof(Form.FirstName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.FirstName), 128));
                }

                if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
                {
                    validation.Errors.Add(nameof(Form.MiddleName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.MiddleName), 128));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Form),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Form)));
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> UpdateValidationAsync(Form newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataForm.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(Form),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Form), newEntity.Id));
                }
                else if (await dataForm.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Form),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(Form)));
                }

                if (newEntity.LastName is null)
                {
                    validation.Errors.Add(nameof(Form.LastName),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Form.LastName)));
                }
                else if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(Form.LastName),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Form.LastName), 3, 128));
                }

                if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
                {
                    validation.Errors.Add(nameof(Form.FirstName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.FirstName), 128));
                }

                if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
                {
                    validation.Errors.Add(nameof(Form.MiddleName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.MiddleName), 128));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Form),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Form)));
            }

            return validation;
        }

        public async Task<ValidationModel<Form>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataForm.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Form),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(Form), id));
            }

            return validation;
        }
    }
}
