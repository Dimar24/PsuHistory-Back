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
    public interface ITypeBurialValidation : IBaseValidation<Guid, TypeBurial>
    { }

    public class TypeBurialValidation : ITypeBurialValidation
    {
        private ValidationModel<TypeBurial> validation;
        private readonly IBaseService<Guid, TypeBurial> dataTypeBurial;

        public TypeBurialValidation(IBaseService<Guid, TypeBurial> dataTypeBurial)
        {
            this.dataTypeBurial = dataTypeBurial;
            validation = new ValidationModel<TypeBurial>();
        }

        public async Task<ValidationModel<TypeBurial>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataTypeBurial.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(TypeBurial), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<TypeBurial>> InsertValidationAsync(TypeBurial newEntity, CancellationToken cancellationToken = default)
        {
            if (await dataTypeBurial.ExistAsync(newEntity, cancellationToken))
            {
                validation.Errors.Add(nameof(TypeBurial), BaseValidation.ObjectExistWithThisData);
            }

            if (newEntity.Name is null)
            {
                validation.Errors.Add(nameof(newEntity.Name), BaseValidation.FieldNotCanBeNull);
            }
            else
            {
                if (newEntity.Name.Length < 3 || newEntity.Name.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.Name), BaseValidation.FieldInvalidLength);
                }
            }

            return validation;
        }

        public async Task<ValidationModel<TypeBurial>> UpdateValidationAsync(TypeBurial newEntity, CancellationToken cancellationToken = default)
        {
            if ((await dataTypeBurial.GetAsync(newEntity.Id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(TypeBurial), BaseValidation.ObjectNotExistById);
            }

            if (await dataTypeBurial.ExistAsync(newEntity, cancellationToken))
            {
                validation.Errors.Add(nameof(TypeBurial), BaseValidation.ObjectExistWithThisData);
            }

            if (newEntity.Name is null)
            {
                validation.Errors.Add(nameof(newEntity.Name), BaseValidation.FieldNotCanBeNull);
            }
            else
            {
                if (newEntity.Name.Length < 3 || newEntity.Name.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.Name), BaseValidation.FieldInvalidLength);
                }
            }

            return validation;
        }

        public async Task<ValidationModel<TypeBurial>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataTypeBurial.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(TypeBurial), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
