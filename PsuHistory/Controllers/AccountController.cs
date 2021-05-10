using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PsuHistory.API.Host.Helpers;
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
        // тестовые данные вместо использования базы данных
        private List<User> users = new List<User>
        {
            new User { Mail = "admin", Password = "secret", Role = new Role() { Name = "admin" } },
            new User { Mail = "user", Password = "secret", Role = new Role() { Name = "user" } }
        };
    
        [HttpPost("/token")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerOperation(Summary = "Search Type Burial", OperationId = "SearchTypeBurial")]
        public async Task<IActionResult> Token(User user)
        {
            var identity = GetIdentity(user.Mail, user.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
    
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    
            var response = new
            {
                access_token = encodedJwt,
                mail = identity.Name
            };
    
            return Ok(response);
        }
    
        private ClaimsIdentity GetIdentity(string mail, string password)
        {
            User user = users.FirstOrDefault(x => x.Mail == mail && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Mail),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
    
            // если пользователя не найдено
            return null;
        }
    }
}
