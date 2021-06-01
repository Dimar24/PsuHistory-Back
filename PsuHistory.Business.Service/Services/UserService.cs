using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public interface IUserService : IBaseService<Guid, User>
    { }

    public class UserService : IUserService
    {
        private readonly IBaseRepository<Guid, User> dataUser;
        private readonly IBaseValidation<Guid, User> userValidation;

        public UserService(
            IBaseRepository<Guid, User> dataTypeVictim,
            IBaseValidation<Guid, User> typeVictimValidation)
        {
            this.dataUser = dataTypeVictim;
            this.userValidation = typeVictimValidation;
        }

        public async Task<ValidationModel<User>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await userValidation.GetValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            validation.Result = await dataUser.GetAsync(id, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var validation = new ValidationModel<IEnumerable<User>>();

            validation.Result = await dataUser.GetAllAsync(cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<User>> InsertAsync(User newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await userValidation.InsertValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            newEntity.CreatedAt = DateTime.Now;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataUser.InsertAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<User>> UpdateAsync(User newEntity, CancellationToken cancellationToken = default)
        {
            var validation = await userValidation.UpdateValidationAsync(newEntity, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var oldEntity = await dataUser.GetAsync(newEntity.Id, cancellationToken);

            newEntity.CreatedAt = oldEntity.CreatedAt;
            newEntity.UpdatedAt = DateTime.Now;

            validation.Result = await dataUser.UpdateAsync(newEntity, cancellationToken);

            return validation;
        }

        public async Task<ValidationModel<User>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var validation = await userValidation.DeleteValidationAsync(id, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            await dataUser.DeleteAsync(id, cancellationToken);

            return validation;
        }
    }
}
