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
    public class ConscriptionPlaceController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseBusinessService<Guid, ConscriptionPlace> conscriptionPlaceService;

        public ConscriptionPlaceController(
            IMapper mapper, 
            IBaseBusinessService<Guid, ConscriptionPlace> conscriptionPlaceService)
        {
            this.mapper = mapper;
            this.conscriptionPlaceService = conscriptionPlaceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await conscriptionPlaceService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await conscriptionPlaceService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateConscriptionPlace createConscriptionPlace)
        {
            var conscriptionPlace = mapper.Map<CreateConscriptionPlace, ConscriptionPlace>(createConscriptionPlace);

            var validation = await conscriptionPlaceService.InsertAsync(conscriptionPlace);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateConscriptionPlace updateConscriptionPlace)
        {
            var conscriptionPlace = mapper.Map<UpdateConscriptionPlace, ConscriptionPlace>(updateConscriptionPlace);

            var validation = await conscriptionPlaceService.UpdateAsync(conscriptionPlace);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await conscriptionPlaceService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
