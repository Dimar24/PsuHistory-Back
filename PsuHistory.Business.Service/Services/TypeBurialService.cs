using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface ITypeBurialService : IBaseService<Guid, TypeBurial>
    { }

    public class TypeBurialService : ITypeBurialService
    {
        private readonly IBaseRepository<Guid, TypeBurial> dataTypeBurial;
        private readonly IBaseValidation<Guid, TypeBurial> typeBurialValidation;

        public TypeBurialService(
            IBaseRepository<Guid, TypeBurial> dataTypeBurial, 
            IBaseValidation<Guid, TypeBurial> typeBurialValidation)
        {
            this.dataTypeBurial = dataTypeBurial;
            this.typeBurialValidation = typeBurialValidation;
        }

        public async Task<ValidationModel<TypeBurial>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await typeBurialValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataTypeBurial.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<TypeBurial>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<TypeBurial>>();

            validation.Result = await dataTypeBurial.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeBurial>> InsertAsync(TypeBurial newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await typeBurialValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataTypeBurial.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeBurial>> UpdateAsync(TypeBurial newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await typeBurialValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataTypeBurial.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataTypeBurial.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeBurial>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await typeBurialValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataTypeBurial.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
