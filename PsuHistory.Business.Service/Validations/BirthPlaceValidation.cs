using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IBirthPlaceValidation : IBaseValidation<Guid, BirthPlace>
    { }

    public class BirthPlaceValidation : IBirthPlaceValidation
    {
        private ValidationModel<BirthPlace> validation;
        private readonly IBaseRepository<Guid, BirthPlace> dataBirthPlace;

        public BirthPlaceValidation(IBaseRepository<Guid, BirthPlace> dataBirthPlace)
        {
            this.dataBirthPlace = dataBirthPlace;
            validation = new ValidationModel<BirthPlace>();
        }

        public async Task<ValidationModel<BirthPlace>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataBirthPlace.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(BirthPlace),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(BirthPlace), id));
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> InsertValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataBirthPlace.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(BirthPlace),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(BirthPlace)));
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(BirthPlace.Place),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(BirthPlace.Place)));
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(BirthPlace.Place),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(BirthPlace.Place), 3, 512));
                }
            }
            else
            {
                validation.Errors.Add(nameof(BirthPlace),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(BirthPlace)));
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> UpdateValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataBirthPlace.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(BirthPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(BirthPlace), newEntity.Id));
                }
                else if (await dataBirthPlace.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(BirthPlace),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(BirthPlace)));
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(BirthPlace.Place),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(BirthPlace.Place)));
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(BirthPlace.Place),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(BirthPlace.Place), 3, 512));
                }
            }
            else
            {
                validation.Errors.Add(nameof(BirthPlace),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(BirthPlace)));
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataBirthPlace.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(BirthPlace),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(BirthPlace), id));
            }

            return validation;
        }
    }
}
