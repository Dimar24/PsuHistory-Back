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
    public class TypeVictimController : AbstractionControllerBase
    {
        private readonly IBaseBusinessService<Guid, TypeVictim> TypeVictimService;
        public TypeVictimController(IBaseBusinessService<Guid, TypeVictim> TypeVictimService)
        {
            this.TypeVictimService = TypeVictimService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var validation = await TypeVictimService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await TypeVictimService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TypeVictim TypeVictim)
        {
            var validation = await TypeVictimService.InsertAsync(TypeVictim);

            return CreateObjectResult(validation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TypeVictim TypeVictim)
        {
            TypeVictim.Id = id;

            var validation = await TypeVictimService.UpdateAsync(TypeVictim);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var validation = await TypeVictimService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
