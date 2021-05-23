using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Models;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IBurialValidation : IBaseValidation<Guid, Burial>
    { }

    public class BurialValidation : IBurialValidation
    {
        private ValidationModel<Burial> validation;
        private readonly IBaseRepository<Guid, Burial> dataBurial;
        private readonly IBaseRepository<Guid, TypeBurial> dataTypeBurial;

        public BurialValidation(IBaseRepository<Guid, Burial> dataBurial, IBaseRepository<Guid, TypeBurial> dataTypeBurial)
        {
            this.dataBurial = dataBurial;
            this.dataTypeBurial = dataTypeBurial;
            validation = new ValidationModel<Burial>();
        }

        public async Task<ValidationModel<Burial>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataBurial.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Burial),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(Burial), id));
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> InsertValidationAsync(Burial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataBurial.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(Burial)));
                }

                if (!await dataTypeBurial.ExistByIdAsync(newEntity.TypeBurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial.TypeBurial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Burial.TypeBurial), newEntity.TypeBurialId));
                }

                if (newEntity.NumberBurial < 0)
                {
                    validation.Errors.Add(nameof(Burial.NumberBurial),
                        string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.NumberBurial)));
                }

                if (newEntity.Location is null)
                {
                    validation.Errors.Add(nameof(Burial.Location),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Burial.Location)));
                }
                else if (newEntity.Location.Length < 3 || newEntity.Location.Length > 512)
                {
                    validation.Errors.Add(nameof(Burial.Location),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Burial.Location), 3, 512));
                }

                if (newEntity.KnownNumber < 0)
                {
                    validation.Errors.Add(nameof(Burial.KnownNumber),
                        string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.KnownNumber)));
                }

                if (newEntity.UnknownNumber < 0)
                {
                    validation.Errors.Add(nameof(Burial.UnknownNumber),
                        string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.UnknownNumber)));
                }

                if (newEntity.Year < 1900 || newEntity.Year > DateTime.Now.Year)
                {
                    validation.Errors.Add(nameof(Burial.Year),
                        string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Year), 1900, DateTime.Now.Year));
                }

                if (newEntity.Latitude < -90.0 || newEntity.Latitude > 90.0)
                {
                    validation.Errors.Add(nameof(Burial.Latitude),
                        string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Latitude), -90.0, 90.0));
                }

                if (newEntity.Longitude < -180.0 || newEntity.Longitude > 180.0)
                {
                    validation.Errors.Add(nameof(Burial.Longitude),
                        string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Longitude), -180.0, 180.0));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Burial),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Burial)));
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> UpdateValidationAsync(Burial newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataBurial.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Burial), newEntity.Id));
                }
                else if (await dataBurial.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(Burial)));
                }

                if (!await dataTypeBurial.ExistByIdAsync(newEntity.TypeBurialId, cancellationToken))
                {
                    validation.Errors.Add(nameof(Burial.TypeBurial),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Burial.TypeBurial), newEntity.TypeBurialId));
                }

                if (newEntity.NumberBurial < 0)
                {
                    validation.Errors.Add(nameof(Burial.NumberBurial),
                        string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.NumberBurial)));
                }

                if (newEntity.Location is null)
                {
                    validation.Errors.Add(nameof(Burial.Location),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Burial.Location)));
                }
                else if (newEntity.Location.Length < 3 || newEntity.Location.Length > 512)
                {
                    validation.Errors.Add(nameof(Burial.Location),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Burial.Location), 3, 512));
                }

                if (newEntity.KnownNumber < 0)
                {
                    validation.Errors.Add(nameof(Burial.KnownNumber),
                        string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.KnownNumber)));
                }

                if (newEntity.UnknownNumber < 0)
                {
                    validation.Errors.Add(nameof(Burial.UnknownNumber),
                        string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.UnknownNumber)));
                }

                if (newEntity.Year < 1900 || newEntity.Year > DateTime.Now.Year)
                {
                    validation.Errors.Add(nameof(Burial.Year),
                        string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Year), 1900, DateTime.Now.Year));
                }

                if (newEntity.Latitude < -90.0 || newEntity.Latitude > 90.0)
                {
                    validation.Errors.Add(nameof(Burial.Latitude),
                        string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Latitude), -90.0, 90.0));
                }

                if (newEntity.Longitude < -180.0 || newEntity.Longitude > 180.0)
                {
                    validation.Errors.Add(nameof(Burial.Longitude),
                        string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Longitude), -180.0, 180.0));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Burial),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Burial)));
            }

            return validation;
        }

        public async Task<ValidationModel<Burial>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataBurial.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(Burial),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(Burial), id));
            }

            return validation;
        }
    }
}
