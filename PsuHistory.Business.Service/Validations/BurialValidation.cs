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
    public interface IBurialValidation : IBaseValidation<Guid, Burial>
    { }

    public class BurialValidation : IBurialValidation
    {
        private ValidationModel<Burial> validation;
        private readonly IBaseService<Guid, Burial> dataBurial;
        private readonly IBaseService<Guid, TypeBurial> dataTypeBurial;

        public BurialValidation(IBaseService<Guid, Burial> dataBurial, IBaseService<Guid, TypeBurial> dataTypeBurial)
        {
            this.dataBurial = dataBurial;
            this.dataTypeBurial = dataTypeBurial;
            validation = new ValidationModel<Burial>();
        }

        public async Task<ValidationModel<Burial>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBurial.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(Burial), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> InsertValidationAsync(Burial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataBurial.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial), BaseValidation.ObjectExistWithThisData);
                }

                if (await dataTypeBurial.ExistByIdAsync(newEntity.TypeBurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial.TypeBurial), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.NumberBurial < 0)
                {
                    validation.Errors.Add(nameof(newEntity.NumberBurial), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Location is null)
                {
                    validation.Errors.Add(nameof(newEntity.Location), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Location.Length < 3 || newEntity.Location.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.Location), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.KnownNumber < 0)
                {
                    validation.Errors.Add(nameof(newEntity.KnownNumber), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.UnknownNumber < 0)
                {
                    validation.Errors.Add(nameof(newEntity.UnknownNumber), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Year < 1940 || newEntity.Year > DateTime.Now.Year)
                {
                    validation.Errors.Add(nameof(newEntity.Year), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Latitude < -90.0 || newEntity.Latitude > 90.0)
                {
                    validation.Errors.Add(nameof(newEntity.Latitude), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Longitude < -180.0 || newEntity.Longitude > 180.0)
                {
                    validation.Errors.Add(nameof(newEntity.Longitude), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(Burial), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> UpdateValidationAsync(Burial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {      
                if (await dataBurial.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial), BaseValidation.ObjectExistWithThisData);
                }

                if (await dataBurial.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial), BaseValidation.ObjectNotExistById);
                }

                if (await dataTypeBurial.ExistByIdAsync(newEntity.TypeBurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial.TypeBurial), BaseValidation.ObjectNotExistById);
                }

                if (newEntity.NumberBurial < 0)
                {
                    validation.Errors.Add(nameof(newEntity.NumberBurial), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Location is null)
                {
                    validation.Errors.Add(nameof(newEntity.Location), BaseValidation.FieldNotCanBeNull);
                }
                else if (newEntity.Location.Length < 3 || newEntity.Location.Length > 512)
                {
                    validation.Errors.Add(nameof(newEntity.Location), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.KnownNumber < 0)
                {
                    validation.Errors.Add(nameof(newEntity.KnownNumber), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.UnknownNumber < 0)
                {
                    validation.Errors.Add(nameof(newEntity.UnknownNumber), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Year < 1940 || newEntity.Year > DateTime.Now.Year)
                {
                    validation.Errors.Add(nameof(newEntity.Year), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Latitude < -90.0 || newEntity.Latitude > 90.0)
                {
                    validation.Errors.Add(nameof(newEntity.Latitude), BaseValidation.FieldInvalidLength);
                }

                if (newEntity.Longitude < -180.0 || newEntity.Longitude > 180.0)
                {
                    validation.Errors.Add(nameof(newEntity.Longitude), BaseValidation.FieldInvalidLength);
                }
            }
            else
            {
                validation.Errors.Add(nameof(Burial), BaseValidation.ObjectNotCanBeNull);
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if ((await dataBurial.GetAsync(id, cancellationToken)) is null)
            {
                validation.Errors.Add(nameof(Burial), BaseValidation.ObjectNotExistById);
            }

            return validation;
        }
    }
}
