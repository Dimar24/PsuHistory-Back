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
            if (!await dataDutyStation.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(DutyStation),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(DutyStation), id));
            }

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> InsertValidationAsync(DutyStation newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataDutyStation.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(DutyStation),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(DutyStation)));
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(DutyStation.Place),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(DutyStation.Place)));
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(DutyStation.Place),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(DutyStation.Place), 3, 512));
                }
            }
            else
            {
                validation.Errors.Add(nameof(DutyStation),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(DutyStation)));
            }

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> UpdateValidationAsync(DutyStation newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataDutyStation.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(DutyStation),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(DutyStation), newEntity.Id));
                }
                else if (await dataDutyStation.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(DutyStation),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(DutyStation)));
                }

                if (newEntity.Place is null)
                {
                    validation.Errors.Add(nameof(DutyStation.Place),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(DutyStation.Place)));
                }
                else if (newEntity.Place.Length < 3 || newEntity.Place.Length > 512)
                {
                    validation.Errors.Add(nameof(DutyStation.Place),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(DutyStation.Place), 3, 512));
                }
            }
            else
            {
                validation.Errors.Add(nameof(DutyStation),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(DutyStation)));
            }

            return validation;
        }

        public async Task<ValidationModel<DutyStation>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataDutyStation.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(DutyStation),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(DutyStation), id));
            }

            return validation;
        }
    }
}
