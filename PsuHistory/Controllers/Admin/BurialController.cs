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
    public class BurialController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, Burial> burialService;

        public BurialController(
            IMapper mapper,
            IBaseService<Guid, Burial> burialService)
        {
            this.mapper = mapper;
            this.burialService = burialService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await burialService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await burialService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateBurial createBurial)
        {
            var burial = mapper.Map<Burial>(createBurial);

            var validation = await burialService.InsertAsync(burial);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateBurial updateBurial)
        {
            var burial = mapper.Map<Burial>(updateBurial);

            var validation = await burialService.UpdateAsync(burial);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await burialService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
