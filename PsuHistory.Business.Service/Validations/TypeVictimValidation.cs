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
    public interface ITypeVictimValidation : IBaseValidation<Guid, TypeVictim>
    { }

    public class TypeVictimValidation : ITypeVictimValidation
    {
        private ValidationModel<TypeVictim> validation;
        private readonly IBaseService<Guid, TypeVictim> dataTypeVictim;

        public TypeVictimValidation(IBaseService<Guid, TypeVictim> dataTypeVictim)
        {
            this.dataTypeVictim = dataTypeVictim;
            validation = new ValidationModel<TypeVictim>();
        }

        public async Task<ValidationModel<TypeVictim>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (await dataTypeVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> InsertValidationAsync(TypeVictim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataTypeVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectExistWithThisData);
                }

                if (newEntity.Name is null)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Name.Length < 3 || newEntity.Name.Length > 512)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> UpdateValidationAsync(TypeVictim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataTypeVictim.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectNotExistById);
                }

                if (await dataTypeVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectExistWithThisData);
                }

                if (newEntity.Name is null)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Name.Length < 3 || newEntity.Name.Length > 512)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name), BaseValidation.FieldInvalidLength);
                }

            }
            else
            {
                validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (await dataTypeVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(TypeVictim), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
