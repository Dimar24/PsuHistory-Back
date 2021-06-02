using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PsuHistory.Controllers.Abstraction;
using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PsuHistory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseAccoutService<Login> accountService;

        public AccountController(
            IMapper mapper,
            IBaseAccoutService<Login> accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
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
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            var validation = await accountService.LoginAsync(login);

            return CreateObjectResult(validation);
        }
    }
}
