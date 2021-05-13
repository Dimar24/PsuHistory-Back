using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using PsuHistory.Data.Service.Services;
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
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> InsertValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            if (await dataBirthPlace.ExistAsync(newEntity, cancellationToken))
            {
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
            {
                validation.Errors.Add(nameof(newEntity.Place), "Ошибка");
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> UpdateValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {

            if ((await dataBirthPlace.GetAsync(newEntity.Id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            if (await dataBirthPlace.ExistAsync(newEntity, cancellationToken))
            {
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
            {
                validation.Errors.Add(nameof(newEntity.Place), "Ошибка");
            }

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBirthPlace.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            return validation;
        }
    }
}
