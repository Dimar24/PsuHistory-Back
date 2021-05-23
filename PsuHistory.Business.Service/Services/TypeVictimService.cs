using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface ITypeVictimService : IBaseService<Guid, TypeVictim>
    { }

    class TypeVictimService : ITypeVictimService
    {
        private readonly IBaseRepository<Guid, TypeVictim> dataTypeVictim;
        private readonly IBaseValidation<Guid, TypeVictim> typeVictimValidation;

        public TypeVictimService(
            IBaseRepository<Guid, TypeVictim> dataTypeVictim, 
            IBaseValidation<Guid, TypeVictim> typeVictimValidation)
        {
            this.dataTypeVictim = dataTypeVictim;
            this.typeVictimValidation = typeVictimValidation;
        }

        public async Task<ValidationModel<TypeVictim>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await typeVictimValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataTypeVictim.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<TypeVictim>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<TypeVictim>>();

            validation.Result = await dataTypeVictim.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> InsertAsync(TypeVictim newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await typeVictimValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataTypeVictim.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> UpdateAsync(TypeVictim newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await typeVictimValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataTypeVictim.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataTypeVictim.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await typeVictimValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataTypeVictim.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
