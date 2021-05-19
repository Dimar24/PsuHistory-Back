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
    public interface IDutyStationValidation : IBaseValidation<Guid, DutyStation>
    { }

    public class DutyStationValidation : IDutyStationValidation
    {
        private ValidationModel<DutyStation> validation;
        private readonly IBaseService<Guid, DutyStation> dataDutyStation;

        public DutyStationValidation(IBaseService<Guid, DutyStation> dataDutyStation)
        {
            this.dataDutyStation = dataDutyStation;
            validation = new ValidationModel<DutyStation>();
        }

        public async Task<ValidationModel<DutyStation>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataDutyStation.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(DutyStation), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> InsertValidationAsync(DutyStation newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataDutyStation.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(DutyStation), BaseValidation.ObjectExistWithThisData);
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(DutyStation.Place), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(DutyStation.Place), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(newEntity), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> UpdateValidationAsync(DutyStation newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if ((await dataDutyStation.GetAsync(newEntity.Id, cancellationToken)) is null)
                {
                    validation.Errors.Add(nameof(DutyStation), BaseValidation.ObjectNotExistById);
                }

                if (await dataDutyStation.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(DutyStation), BaseValidation.ObjectExistWithThisData);
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(DutyStation.Place), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(DutyStation.Place), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(DutyStation), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataDutyStation.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(DutyStation), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
