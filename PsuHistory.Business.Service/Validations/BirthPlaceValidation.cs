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
    public interface IBirthPlaceValidation : IBaseValidation<Guid, BirthPlace>
    { }

    public class BirthPlaceValidation : IBirthPlaceValidation
    {
        private ValidationModel<BirthPlace> validation;
        private readonly IBaseService<Guid, BirthPlace> dataBirthPlace;

        public BirthPlaceValidation(IBaseService<Guid, BirthPlace> dataBirthPlace)
        {
            this.dataBirthPlace = dataBirthPlace;
            validation = new ValidationModel<BirthPlace>();
        }

        public async Task<ValidationModel<BirthPlace>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBirthPlace.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(BirthPlace), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> InsertValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataBirthPlace.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectExistWithThisData);
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(newEntity.Place), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.Place), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> UpdateValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if ((await dataBirthPlace.GetAsync(newEntity.Id, cancellationToken)) is null)
                {
                    validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotExistById);
                }

                if (await dataBirthPlace.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectExistWithThisData);
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(newEntity.Place), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.Place), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBirthPlace.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(BirthPlace), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
