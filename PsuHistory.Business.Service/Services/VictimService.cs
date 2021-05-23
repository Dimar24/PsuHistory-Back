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
    public interface IVictimService : IBaseService<Guid, Victim>
    { }

    public class VictimService : IVictimService
    {
        private readonly IBaseRepository<Guid, Victim> dataVictim;
        private readonly IBaseValidation<Guid, Victim> victimValidation;

        public VictimService(
            IBaseRepository<Guid, Victim> dataVictim,
            IBaseValidation<Guid, Victim> victimValidation)
        {
            this.dataVictim = dataVictim;
            this.victimValidation = victimValidation;
        }

        public async Task<ValidationModel<Victim>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await victimValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataVictim.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<Victim>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<Victim>>();

            validation.Result = await dataVictim.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<Victim>> InsertAsync(Victim newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await victimValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataVictim.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<Victim>> UpdateAsync(Victim newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await victimValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataVictim.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataVictim.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<Victim>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await victimValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataVictim.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
