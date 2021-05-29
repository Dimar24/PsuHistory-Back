using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PsuHistory.API.Host.Options;
using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Users;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PsuHistory.API.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;

        public AccountController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }

        // тестовые данные вместо использования базы данных
        private List<User> users = new List<User>
        {
            new User { Mail = "Admin", Password = "secret", Role = new Role() { Name = "Admin" } },
            new User { Mail = "Moderator", Password = "secret", Role = new Role() { Name = "Moderator" } },
            new User { Mail = "user", Password = "secret", Role = new Role() { Name = "user" } }
        };

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = AuthenticateUser(login.Mail, login.Password);

            if(user is null)
            {
                return Unauthorized();
            }

            var token = GenerateJWT(user);

            return Ok(token);
        } 

        private User AuthenticateUser(string mail, string password)
        {
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
