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
    public class BirthPlaceController : AbstractionControllerBase
    {
        private readonly IBaseBusinessService<Guid, BirthPlace> birthPlaceService;
        public BirthPlaceController(IBaseBusinessService<Guid, BirthPlace> birthPlaceService)
        {
            this.birthPlaceService = birthPlaceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var validation = await birthPlaceService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await birthPlaceService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BirthPlace birthPlace)
        {
            var validation = await birthPlaceService.InsertAsync(birthPlace);

            return CreateObjectResult(validation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] BirthPlace birthPlace)
        {
            var validation = await birthPlaceService.UpdateAsync(birthPlace);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var validation = await birthPlaceService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
