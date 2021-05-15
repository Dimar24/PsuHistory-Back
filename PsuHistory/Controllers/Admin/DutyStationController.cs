using Microsoft.AspNetCore.Mvc;
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
        private readonly IBaseBusinessService<Guid, DutyStation> DutyStationService;
        public DutyStationController(IBaseBusinessService<Guid, DutyStation> DutyStationService)
        {
            this.DutyStationService = DutyStationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var validation = await DutyStationService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await DutyStationService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DutyStation DutyStation)
        {
            var validation = await DutyStationService.InsertAsync(DutyStation);

            return CreateObjectResult(validation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] DutyStation DutyStation)
        {
            DutyStation.Id = id;

            var validation = await DutyStationService.UpdateAsync(DutyStation);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var validation = await DutyStationService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
