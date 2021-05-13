using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.Service.Interfaces;
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

        public async Task<BirthPlace> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.GetValidationAsync(id, cancellationToken);

            if(!validation.IsValid)
            {
                return null;
            }

            return await dataBirthPlace.GetAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<BirthPlace>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dataBirthPlace.GetAllAsync(cancellationToken);
        }

        public async Task<IActionResult> InsertAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return null;
            }

            return Ok(await dataBirthPlace.InsertAsync(newEntity, cancellationToken));
        }

        public async Task<IActionResult> UpdateAsync(BirthPlace newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return null;
            }

            return Ok(await dataBirthPlace.InsertAsync(newEntity, cancellationToken);)
        }

        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await birthPlaceValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return;
            }

            await Ok(dataBirthPlace.DeleteAsync(id, cancellationToken));
        }
    }
}
