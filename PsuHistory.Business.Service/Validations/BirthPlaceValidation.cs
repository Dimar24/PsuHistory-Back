using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IBirthPlaceValidation : IBaseValidation<Guid, BirthPlace>
    { }

    class BirthPlaceValidation : IBirthPlaceValidation
    {
        private ValidationModel validation;
        private readonly BirthPlaceService dataBirthPlace;

        public BirthPlaceValidation(BirthPlaceService dataBirthPlace)
        {
            this.dataBirthPlace = dataBirthPlace;
            validation = new ValidationModel();
        }

        public async Task<ValidationModel> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBirthPlace.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            return validation;
        }

        public async Task<ValidationModel> InsertValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
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

        public async Task<ValidationModel> UpdateValidationAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
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

        public async Task<ValidationModel> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBirthPlace.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(BirthPlace), "Ошибка");
            }

            return validation;
        }
    }
}
