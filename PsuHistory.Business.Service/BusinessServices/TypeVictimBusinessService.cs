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
    public interface ITypeVictimBusinessService : IBaseBusinessService<Guid, TypeVictim>
    { }

    class TypeVictimBusinessService : ITypeVictimBusinessService
    {
        private readonly IBaseService<Guid, TypeVictim> dataTypeVictim;
        private readonly IBaseValidation<Guid, TypeVictim> TypeVictimValidation;

        public TypeVictimBusinessService(
            IBaseService<Guid, TypeVictim> dataTypeVictim, 
            IBaseValidation<Guid, TypeVictim> TypeVictimValidation)
        {
            this.dataTypeVictim = dataTypeVictim;
            this.TypeVictimValidation = TypeVictimValidation;
        }

        public async Task<ValidationModel<TypeVictim>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await TypeVictimValidation.GetValidationAsync(id, cancellationToken);

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
            var validation = await TypeVictimValidation.InsertValidationAsync(newEntity, cancellationToken);

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
            var validation = await TypeVictimValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataTypeVictim.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<TypeVictim>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await TypeVictimValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataTypeVictim.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
