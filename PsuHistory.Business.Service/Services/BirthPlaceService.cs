using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface IBirthPlaceService : IBaseService<Guid, BirthPlace>
    { }

    public class BirthPlaceService : IBirthPlaceService
    {
        private readonly IBaseRepository<Guid, BirthPlace> dataBirthPlace;
        private readonly IBaseValidation<Guid, BirthPlace> birthPlaceValidation;

        public BirthPlaceService(
            IBaseRepository<Guid, BirthPlace> dataBirthPlace, 
            IBaseValidation<Guid, BirthPlace> birthPlaceValidation)
        {
            this.dataBirthPlace = dataBirthPlace;
            this.birthPlaceValidation = birthPlaceValidation;
        }

        public async Task<ValidationModel<BirthPlace>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.GetValidationAsync(id, cancellationToken);

            if(!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataBirthPlace.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<BirthPlace>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<BirthPlace>>();

            validation.Result = await dataBirthPlace.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> InsertAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataBirthPlace.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> UpdateAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataBirthPlace.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataBirthPlace.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<BirthPlace>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataBirthPlace.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
