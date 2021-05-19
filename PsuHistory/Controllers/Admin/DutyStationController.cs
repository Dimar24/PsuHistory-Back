using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Controllers.Abstraction;
using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Threading.Tasks;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class DutyStationController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseBusinessService<Guid, DutyStation> dutyStationService;

        public DutyStationController(
            IMapper mapper, 
            IBaseBusinessService<Guid, DutyStation> dutyStationService)
        {
            this.mapper = mapper;
            this.dutyStationService = dutyStationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await dutyStationService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await dutyStationService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateDutyStation createDutyStation)
        {
            var dutyStation = mapper.Map<DutyStation>(createDutyStation);

            var validation = await dutyStationService.InsertAsync(dutyStation);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateDutyStation updateDutyStation)
        {
            var dutyStation = mapper.Map<DutyStation>(updateDutyStation);

            var validation = await dutyStationService.UpdateAsync(dutyStation);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await dutyStationService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
