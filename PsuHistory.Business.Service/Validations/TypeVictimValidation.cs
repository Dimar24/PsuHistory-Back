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
    public interface ITypeVictimValidation : IBaseValidation<Guid, TypeVictim>
    { }

    public class TypeVictimValidation : ITypeVictimValidation
    {
        private ValidationModel<TypeVictim> validation;
        private readonly IBaseRepository<Guid, TypeVictim> dataTypeVictim;

        public TypeVictimValidation(IBaseRepository<Guid, TypeVictim> dataTypeVictim)
        {
            this.dataTypeVictim = dataTypeVictim;
            validation = new ValidationModel<TypeVictim>();
        }

        public async Task<ValidationModel<TypeVictim>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataTypeVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(TypeVictim),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(TypeVictim), id));
            }

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> InsertValidationAsync(TypeVictim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataTypeVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(TypeVictim),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(TypeVictim)));
                }

                if (newEntity.Name is null)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(TypeVictim.Name)));
                }
                else if (newEntity.Name.Length < 3 || newEntity.Name.Length > 128)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(TypeVictim.Name), 3, 128));
                }
            }
            else
            {
                validation.Errors.Add(nameof(TypeVictim),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(TypeVictim)));
            }

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> UpdateValidationAsync(TypeVictim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataTypeVictim.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(TypeVictim),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(TypeVictim), newEntity.Id));
                }
                else if (await dataTypeVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(TypeVictim),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(TypeVictim)));
                }

                if (newEntity.Name is null)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(TypeVictim.Name)));
                }
                else if (newEntity.Name.Length < 3 || newEntity.Name.Length > 128)
                {
                    validation.Errors.Add(nameof(TypeVictim.Name),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(TypeVictim.Name), 3, 128));
                }
            }
            else
            {
                validation.Errors.Add(nameof(TypeVictim),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(TypeVictim)));
            }

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataTypeVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(TypeVictim),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(TypeVictim), id));
            }

            return validation;
        }
    }
}
