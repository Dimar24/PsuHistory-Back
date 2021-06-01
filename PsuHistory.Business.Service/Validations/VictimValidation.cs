using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IVictimValidation : IBaseValidation<Guid, Victim>
    { }

    public class VictimValidation : IVictimValidation
    {
        private ValidationModel<Victim> validation;
        private readonly IBaseRepository<Guid, Victim> dataVictim;
        private readonly IBaseRepository<Guid, TypeVictim> dataTypeVictim;
        private readonly IBaseRepository<Guid, DutyStation> dataDutyStation;
        private readonly IBaseRepository<Guid, BirthPlace> dataBirthPlace;
        private readonly IBaseRepository<Guid, ConscriptionPlace> dataConscriptionPlace;
        private readonly IBaseRepository<Guid, Burial> dataBurial;

        public VictimValidation(
            IBaseRepository<Guid, Victim> dataVictim,
            IBaseRepository<Guid, TypeVictim> dataTypeVictim,
            IBaseRepository<Guid, DutyStation> dataDutyStation,
            IBaseRepository<Guid, BirthPlace> dataBirthPlace,
            IBaseRepository<Guid, ConscriptionPlace> dataConscriptionPlace,
            IBaseRepository<Guid, Burial> dataBurial)
        {
            this.dataVictim = dataVictim;
            this.dataTypeVictim = dataTypeVictim;
            this.dataDutyStation = dataDutyStation;
            this.dataBirthPlace = dataBirthPlace;
            this.dataConscriptionPlace = dataConscriptionPlace;
            this.dataBurial = dataBurial;
            validation = new ValidationModel<Victim>();
        }

        public async Task<ValidationModel<Victim>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Victim),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(Victim), id));
            }

            return validation;
        }

        public async Task<ValidationModel<Victim>> InsertValidationAsync(Victim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(Victim)));
                }

                if (!await dataTypeVictim.ExistByIdAsync(newEntity.TypeVictimId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.TypeVictim),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.TypeVictim), newEntity.TypeVictimId));
                }

                if (!await dataDutyStation.ExistByIdAsync(newEntity.DutyStationId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.DutyStation),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.DutyStation), newEntity.DutyStationId));
                }

                if (!await dataBirthPlace.ExistByIdAsync(newEntity.BirthPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.BirthPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.BirthPlace), newEntity.BirthPlaceId));
                }

                if (!await dataConscriptionPlace.ExistByIdAsync(newEntity.ConscriptionPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.ConscriptionPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.ConscriptionPlace), newEntity.ConscriptionPlaceId));
                }

                if (!await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.Burial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.Burial), newEntity.BurialId));
                }

                if (newEntity.LastName is null)
                {
                    validation.Errors.Add(nameof(Victim.LastName),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Victim.LastName)));
                }
                else if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.LastName),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Victim.LastName), 3, 128));
                }

                if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.FirstName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.FirstName), 128));
                }

                if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.MiddleName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.MiddleName), 128));
                }

                if (newEntity.DateOfBirth is not null && newEntity.DateOfBirth.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.DateOfBirth),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfBirth), 64));
                }

                if (newEntity.DateOfDeath is not null && newEntity.DateOfDeath.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.DateOfDeath),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfDeath), 64));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Victim),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Victim)));
            }

            return validation;
        }

        public async Task<ValidationModel<Victim>> UpdateValidationAsync(Victim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataVictim.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim), newEntity.Id));
                }
                else if (await dataVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(Victim)));
                }

                if (!await dataTypeVictim.ExistByIdAsync(newEntity.TypeVictimId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.TypeVictim),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.TypeVictim), newEntity.TypeVictimId));
                }

                if (!await dataDutyStation.ExistByIdAsync(newEntity.DutyStationId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.DutyStation),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.DutyStation), newEntity.DutyStationId));
                }

                if (!await dataBirthPlace.ExistByIdAsync(newEntity.BirthPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.BirthPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.BirthPlace), newEntity.BirthPlaceId));
                }

                if (!await dataConscriptionPlace.ExistByIdAsync(newEntity.ConscriptionPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.ConscriptionPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.ConscriptionPlace), newEntity.ConscriptionPlaceId));
                }

                if (!await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.Burial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.Burial), newEntity.BurialId));
                }

                if (newEntity.LastName is null)
                {
                    validation.Errors.Add(nameof(Victim.LastName),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Victim.LastName)));
                }
                else if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.LastName),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Victim.LastName), 3, 128));
                }

                if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.FirstName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.FirstName), 128));
                }

                if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.MiddleName),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.MiddleName), 128));
                }

                if (newEntity.DateOfBirth is not null && newEntity.DateOfBirth.Length > 64)
                {
                    validation.Errors.Add(nameof(Victim.DateOfBirth),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfBirth), 64));
                }

                if (newEntity.DateOfDeath is not null && newEntity.DateOfDeath.Length > 64)
                {
                    validation.Errors.Add(nameof(Victim.DateOfDeath),
                        string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfDeath), 64));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Victim),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Victim)));
            }

            return validation;
        }

        public async Task<ValidationModel<Victim>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Victim),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(Victim), id));
            }

            return validation;
        }
    }
}
