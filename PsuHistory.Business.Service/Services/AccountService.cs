using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Common.Options;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Services
{
    public class AccountService : IBaseAccoutService<Login>
    {
        private readonly IOptions<AuthOptions> authOptions;
        private readonly IBaseRepository<Guid, User> dataUser;
        private readonly IBaseAccoutValidation<Login> loginValidation;

        public AccountService(
            IOptions<AuthOptions> authOptions,
            IBaseRepository<Guid, User> dataUser,
            IBaseAccoutValidation<Login> loginValidation)
        {
            this.authOptions = authOptions;
            this.dataUser = dataUser;
            this.loginValidation = loginValidation;
        }

        public async Task<ValidationModel<Login>> LoginAsync(Login login, CancellationToken cancellationToken = default(CancellationToken))
        {
            var validation = await loginValidation.LoginAsync(login, cancellationToken);

            if (!validation.IsValid)
            {
                return validation;
            }

            var user = await AuthenticateUser(login.Mail, login.Password);

            var token = GenerateJWT(user);

            return validation;
        }

        private async Task<User> AuthenticateUser(string mail, string password)
        {
            var users = await dataUser.GetAllAsync();

            return users.SingleOrDefault(u => u.Mail == mail && u.Password == password);
        }

        private string GenerateJWT(User user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Mail),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", user.Role.Name)
            };

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
