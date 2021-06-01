using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public interface IUserValidation : IBaseValidation<Guid, User>
    { }

    public class UserValidation : IUserValidation
    {
        private ValidationModel<User> validation;
        private readonly IBaseRepository<Guid, User> dataUser;
        private readonly IBaseRepository<Guid, Role> dataRole;

        public UserValidation(IBaseRepository<Guid, User> dataUser, IBaseRepository<Guid, Role> dataRole)
        {
            this.dataUser = dataUser;
            this.dataRole = dataRole;
            validation = new ValidationModel<User>();
        }

        public async Task<ValidationModel<User>> GetValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataUser.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(User),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(User), id));
            }

            return validation;
        }

        public async Task<ValidationModel<User>> InsertValidationAsync(User newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (await dataUser.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(User),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(User)));
                }

                if (!await dataRole.ExistByIdAsync(newEntity.RoleId, cancellationToken))
                {
                    validation.Errors.Add(nameof(User.Role),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(User.Role), newEntity.RoleId));
                }

                if (newEntity.Mail is null)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)));
                }
                else if (newEntity.Mail.Length < 3 || newEntity.Mail.Length > 256)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 3, 256));
                }

                if (newEntity.Password is null)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)));
                }
                else if (newEntity.Password.Length < 6 || newEntity.Password.Length > 64)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 6, 64));
                }
            }
            else
            {
                validation.Errors.Add(nameof(User),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(User)));
            }

            return validation;
        }

        public async Task<ValidationModel<User>> UpdateValidationAsync(User newEntity, CancellationToken cancellationToken = default)
        {
            if (newEntity is not null)
            {
                if (!await dataUser.ExistByIdAsync(newEntity.Id, cancellationToken))
                {
                    validation.Errors.Add(nameof(User),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(User), newEntity.Id));
                }
                else if (await dataUser.ExistAsync(newEntity, cancellationToken))
                {
                    validation.Errors.Add(nameof(User),
                        string.Format(BaseValidation.ObjectExistWithThisData, nameof(User)));
                }

                if (!await dataRole.ExistByIdAsync(newEntity.RoleId, cancellationToken))
                {
                    validation.Errors.Add(nameof(User.Role),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(User.Role), newEntity.RoleId));
                }

                if (newEntity.Mail is null)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)));
                }
                else if (newEntity.Mail.Length < 3 || newEntity.Mail.Length > 256)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 3, 256));
                }

                if (newEntity.Password is null)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)));
                }
                else if (newEntity.Password.Length < 6 || newEntity.Password.Length > 64)
                {
                    validation.Errors.Add(nameof(User.Mail),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 6, 64));
                }
            }
            else
            {
                validation.Errors.Add(nameof(User),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(User)));
            }

            return validation;
        }

        public async Task<ValidationModel<User>> DeleteValidationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!await dataUser.ExistByIdAsync(id, cancellationToken))
            {
                validation.Errors.Add(nameof(User),
                    string.Format(BaseValidation.ObjectNotExistById, nameof(User), id));
            }

            return validation;
        }
    }
}
