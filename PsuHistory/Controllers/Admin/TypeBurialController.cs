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
    public class TypeBurialController : AbstractionControllerBase
    {
        private readonly IBaseBusinessService<Guid, TypeBurial> TypeBurialService;
        public TypeBurialController(IBaseBusinessService<Guid, TypeBurial> TypeBurialService)
        {
            this.TypeBurialService = TypeBurialService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var validation = await TypeBurialService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await TypeBurialService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TypeBurial TypeBurial)
        {
            var validation = await TypeBurialService.InsertAsync(TypeBurial);

            return CreateObjectResult(validation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TypeBurial TypeBurial)
        {
            TypeBurial.Id = id;

            var validation = await TypeBurialService.UpdateAsync(TypeBurial);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var validation = await TypeBurialService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
