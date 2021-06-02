using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.Controllers.Abstraction;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Threading.Tasks;

namespace PsuHistory.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin, Moderator")]
    [Route("api/admin/[controller]")]
    public class ConscriptionPlaceController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, ConscriptionPlace> conscriptionPlaceService;

        public ConscriptionPlaceController(
            IMapper mapper,
            IBaseService<Guid, ConscriptionPlace> conscriptionPlaceService)
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
        public async Task<IActionResult> PostAsync([FromForm] CreateConscriptionPlace createConscriptionPlace)
        {
            var conscriptionPlace = mapper.Map<ConscriptionPlace>(createConscriptionPlace);

            var validation = await conscriptionPlaceService.InsertAsync(conscriptionPlace);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateConscriptionPlace updateConscriptionPlace)
        {
            var conscriptionPlace = mapper.Map<ConscriptionPlace>(updateConscriptionPlace);

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
