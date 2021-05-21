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
    public interface IDutyStationBusinessService : IBaseBusinessService<Guid, DutyStation>
    { }

    public class DutyStationBusinessService : IDutyStationBusinessService
    {
        private readonly IBaseService<Guid, DutyStation> dataDutyStation;
        private readonly IBaseValidation<Guid, DutyStation> dutyStationValidation;

        public DutyStationBusinessService(
            IBaseService<Guid, DutyStation> dataDutyStation, 
            IBaseValidation<Guid, DutyStation> dutyStationValidation)
        {
            this.dataDutyStation = dataDutyStation;
            this.dutyStationValidation = dutyStationValidation;
        }

        public async Task<ValidationModel<DutyStation>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await dutyStationValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataDutyStation.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<DutyStation>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<DutyStation>>();

            validation.Result = await dataDutyStation.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> InsertAsync(DutyStation newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await dutyStationValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataDutyStation.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> UpdateAsync(DutyStation newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await dutyStationValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataDutyStation.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataDutyStation.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await dutyStationValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataDutyStation.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
