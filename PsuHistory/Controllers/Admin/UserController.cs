using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.API.Controllers.Abstraction;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Users;
using System;
using System.Threading.Tasks;

namespace PsuHistory.API.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    public class UserController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, User> userService;

        public UserController(
            IMapper mapper,
            IBaseService<Guid, User> userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await userService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await userService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateUser createUser)
        {
            var user = mapper.Map<User>(createUser);

            var validation = await userService.InsertAsync(user);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateUser updateUser)
        {
            var user = mapper.Map<User>(updateUser);

            var validation = await userService.UpdateAsync(user);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await userService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
