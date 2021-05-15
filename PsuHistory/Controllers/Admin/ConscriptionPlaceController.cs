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
    public class ConscriptionPlaceController : AbstractionControllerBase
    {
        private readonly IBaseBusinessService<Guid, ConscriptionPlace> ConscriptionPlaceService;
        public ConscriptionPlaceController(IBaseBusinessService<Guid, ConscriptionPlace> ConscriptionPlaceService)
        {
            this.ConscriptionPlaceService = ConscriptionPlaceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var validation = await ConscriptionPlaceService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await ConscriptionPlaceService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ConscriptionPlace ConscriptionPlace)
        {
            var validation = await ConscriptionPlaceService.InsertAsync(ConscriptionPlace);

            return CreateObjectResult(validation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] ConscriptionPlace ConscriptionPlace)
        {
            ConscriptionPlace.Id = id;

            var validation = await ConscriptionPlaceService.UpdateAsync(ConscriptionPlace);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var validation = await ConscriptionPlaceService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
