using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Threading.Tasks;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class BirthPlaceController : ControllerBase
    {
        private readonly IBaseBusinessService<Guid, BirthPlace> birthPlaceService;
        public BirthPlaceController(IBaseBusinessService<Guid, BirthPlace> birthPlaceService)
        {
            this.birthPlaceService = birthPlaceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await birthPlaceService.GetAsync(id);

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await birthPlaceService.GetAllAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BirthPlace birthPlace)
        {
            var data = birthPlaceService.InsertAsync(birthPlace);

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] BirthPlace birthPlace)
        {
            var data = await birthPlaceService.UpdateAsync(birthPlace);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var data = await birthPlaceService.DeleteAsync(id);

            return Ok(data);
        }
    }
}
