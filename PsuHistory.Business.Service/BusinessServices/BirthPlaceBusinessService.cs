using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using PsuHistory.Data.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface IBirthPlaceBusinessService : IBaseBusinessService<Guid, BirthPlace>
    { }

    class BirthPlaceBusinessService : IBirthPlaceBusinessService
    {
        private readonly IBaseService<Guid, BirthPlace> dataBirthPlace;
        private readonly IBaseValidation<Guid, BirthPlace> birthPlaceValidation;

        public BirthPlaceBusinessService(IBaseService<Guid, BirthPlace> dataBirthPlace, IBaseValidation<Guid, BirthPlace> birthPlaceValidation)
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
