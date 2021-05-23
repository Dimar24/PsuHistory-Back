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
    public interface IConscriptionPlaceValidation : IBaseValidation<Guid, ConscriptionPlace>
    { }

    public class ConscriptionPlaceValidation : IConscriptionPlaceValidation
    {
        private ValidationModel<ConscriptionPlace> validation;
        private readonly IBaseRepository<Guid, ConscriptionPlace> dataConscriptionPlace;

        public ConscriptionPlaceValidation(IBaseRepository<Guid, ConscriptionPlace> dataConscriptionPlace)
        {
            this.dataConscriptionPlace = dataConscriptionPlace;
            validation = new ValidationModel<ConscriptionPlace>();
        }

        public async Task<ValidationModel<ConscriptionPlace>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataConscriptionPlace.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(ConscriptionPlace),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(ConscriptionPlace), id));
            }

            return validation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> InsertValidationAsync(ConscriptionPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataConscriptionPlace.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(ConscriptionPlace),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(ConscriptionPlace)));
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(ConscriptionPlace.Place),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(ConscriptionPlace.Place)));
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(ConscriptionPlace.Place),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(ConscriptionPlace.Place), 3, 512));
                }
            }
            else
            {
                validation.Errors.Add(nameof(ConscriptionPlace),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(ConscriptionPlace)));
            }

            return validation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> UpdateValidationAsync(ConscriptionPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataConscriptionPlace.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(ConscriptionPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(ConscriptionPlace), newEntity.Id));
                }
                else if (await dataConscriptionPlace.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(ConscriptionPlace),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(ConscriptionPlace)));
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(ConscriptionPlace.Place),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(ConscriptionPlace.Place)));
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(ConscriptionPlace.Place),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(ConscriptionPlace.Place), 3, 512));
                }
            }
            else
            {
                validation.Errors.Add(nameof(ConscriptionPlace),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(ConscriptionPlace)));
            }

            return validation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataConscriptionPlace.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(ConscriptionPlace),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(ConscriptionPlace), id));
            }

            return validation;
        }
    }
}
