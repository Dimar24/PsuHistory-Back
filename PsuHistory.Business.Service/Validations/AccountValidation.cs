using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Validations
{
    public class AccountValidation : IBaseAccoutValidation<Login>
    {
        private ValidationModel<Login> validation;
        private readonly IBaseRepository<Guid, User> dataUser;

        public AccountValidation(IBaseRepository<Guid, User> dataUser)
        {
            this.dataUser = dataUser;
            validation = new ValidationModel<Login>();
        }

        public async Task<ValidationModel<Login>> LoginAsync(Login login, CancellationToken cancellationToken = default)
        {
            if (login is not null)
            {
                if (login.Mail is null)
                {
                    validation.Errors.Add(nameof(Login.Mail),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Login.Mail)));
                }
                else if (login.Mail.Length < 3 || login.Mail.Length > 256)
                {
                    validation.Errors.Add(nameof(Login.Mail),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Login.Mail), 3, 256));
                }

                if (login.Password is null)
                {
                    validation.Errors.Add(nameof(Login.Password),
                        string.Format(BaseValidation.FieldNotCanBeNull, nameof(Login.Password)));
                }
                else if (login.Password.Length < 6 || login.Password.Length > 64)
                {
                    validation.Errors.Add(nameof(Login.Password),
                        string.Format(BaseValidation.FieldInvalidLength, nameof(Login.Password), 6, 64));
                }

                var users = await dataUser.GetAllAsync();
                if (!users.Any(u => u.Mail == login.Mail && u.Password == login.Password))
                {
                    validation.Errors.Add(nameof(Login),"error");
                        //string.Format(BaseValidation.FieldNotCanBeNull, nameof(Login.Password)));
                }
            }
            else
            {
                validation.Errors.Add(nameof(Login),
                    string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Login)));
            }

            return validation;
        }
    }
}
