using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.API.Controllers.Abstraction;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Threading.Tasks;

namespace PsuHistory.API.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin, Moderator")]
    [Route("api/admin/[controller]")]
    public class VictimController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, Victim> victimService;

        public VictimController(
            IMapper mapper,
            IBaseService<Guid, Victim> victimService)
        {
            this.mapper = mapper;
            this.victimService = victimService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await victimService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await victimService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateVictim createVictim)
        {
            var victim = mapper.Map<Victim>(createVictim);

            var validation = await victimService.InsertAsync(victim);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateVictim updateVictim)
        {
            var victim = mapper.Map<Victim>(updateVictim);

            var validation = await victimService.UpdateAsync(victim);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await victimService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
