using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IVictimValidation : IBaseValidation<Guid, Victim>
    { }

    public class VictimValidation : IVictimValidation
    {
        private ValidationModel<Victim> validation;
        private readonly IBaseService<Guid, Victim> dataVictim;
        private readonly IBaseService<Guid, TypeVictim> dataTypeVictim;
        private readonly IBaseService<Guid, DutyStation> dataDutyStation;
        private readonly IBaseService<Guid, BirthPlace> dataBirthPlace;
        private readonly IBaseService<Guid, ConscriptionPlace> dataConscriptionPlace;
        private readonly IBaseService<Guid, Burial> dataBurial;

        public VictimValidation(
            IBaseService<Guid, Victim> dataVictim,
            IBaseService<Guid, TypeVictim> dataTypeVictim,
            IBaseService<Guid, DutyStation> dataDutyStation,
            IBaseService<Guid, BirthPlace> dataBirthPlace,
            IBaseService<Guid, ConscriptionPlace> dataConscriptionPlace,
            IBaseService<Guid, Burial> dataBurial)
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
            if (await dataVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Victim), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<Victim>> InsertValidationAsync(Victim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim), BaseValidation.ObjectExistWithThisData);
                }

                if (await dataTypeVictim.ExistByIdAsync(newEntity.TypeVictimId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.TypeVictim), BaseValidation.ObjectNotExistById);
                }

                if (await dataDutyStation.ExistByIdAsync(newEntity.DutyStationId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.DutyStation), BaseValidation.ObjectNotExistById);
                }

                if (await dataBirthPlace.ExistByIdAsync(newEntity.BirthPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.BirthPlace), BaseValidation.ObjectNotExistById);
                }

                if (await dataConscriptionPlace.ExistByIdAsync(newEntity.ConscriptionPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.ConscriptionPlace), BaseValidation.ObjectNotExistById);
                }

                if (await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.Burial), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.LastName is null)
                {
                    validation.Errors.Add(nameof(Victim.LastName), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.LastName), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.FirstName), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.MiddleName), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.DateOfBirth is not null && newEntity.DateOfBirth.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.DateOfBirth), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.DateOfDeath is not null && newEntity.DateOfDeath.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.DateOfDeath), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(Victim), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<Victim>> UpdateValidationAsync(Victim newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataVictim.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim), BaseValidation.ObjectNotExistById);
                }

                if (await dataVictim.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim), BaseValidation.ObjectExistWithThisData);
                }

                if (await dataTypeVictim.ExistByIdAsync(newEntity.TypeVictimId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.TypeVictim), BaseValidation.ObjectNotExistById);
                }

                if (await dataDutyStation.ExistByIdAsync(newEntity.DutyStationId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.DutyStation), BaseValidation.ObjectNotExistById);
                }

                if (await dataBirthPlace.ExistByIdAsync(newEntity.BirthPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.BirthPlace), BaseValidation.ObjectNotExistById);
                }

                if (await dataConscriptionPlace.ExistByIdAsync(newEntity.ConscriptionPlaceId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.ConscriptionPlace), BaseValidation.ObjectNotExistById);
                }

                if (await dataBurial.ExistByIdAsync(newEntity.BurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Victim.Burial), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.LastName is null)
                {
                    validation.Errors.Add(nameof(Victim.LastName), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.LastName.Length < 3 || newEntity.LastName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.LastName), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.FirstName is not null && newEntity.FirstName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.FirstName), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.MiddleName is not null && newEntity.MiddleName.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.MiddleName), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.DateOfBirth is not null && newEntity.DateOfBirth.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.DateOfBirth), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.DateOfDeath is not null && newEntity.DateOfDeath.Length > 128)
                {
                    validation.Errors.Add(nameof(Victim.DateOfDeath), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(Victim), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<Victim>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (await dataVictim.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Victim), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
