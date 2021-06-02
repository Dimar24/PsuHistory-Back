using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Controllers.Abstraction;
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

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            var validation = await accountService.LoginAsync(login);

            return CreateObjectResult(validation);
        }
    }
}
