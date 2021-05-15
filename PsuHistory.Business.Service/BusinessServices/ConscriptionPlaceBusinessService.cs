using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.BusinessServices
{
    public interface IConscriptionPlaceBusinessService : IBaseBusinessService<Guid, ConscriptionPlace>
    { }

    public class ConscriptionPlaceBusinessService : IConscriptionPlaceBusinessService
    {
        private readonly IBaseService<Guid, ConscriptionPlace> dataConscriptionPlace;
        private readonly IBaseValidation<Guid, ConscriptionPlace> ConscriptionPlaceValidation;

        public ConscriptionPlaceBusinessService(IBaseService<Guid, ConscriptionPlace> dataConscriptionPlace, IBaseValidation<Guid, ConscriptionPlace> ConscriptionPlaceValidation)
        {
            this.dataConscriptionPlace = dataConscriptionPlace;
            this.ConscriptionPlaceValidation = ConscriptionPlaceValidation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await ConscriptionPlaceValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataConscriptionPlace.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<ConscriptionPlace>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<ConscriptionPlace>>();

            validation.Result = await dataConscriptionPlace.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> InsertAsync(ConscriptionPlace newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await ConscriptionPlaceValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataConscriptionPlace.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> UpdateAsync(ConscriptionPlace newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await ConscriptionPlaceValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataConscriptionPlace.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<ConscriptionPlace>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await ConscriptionPlaceValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataConscriptionPlace.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
