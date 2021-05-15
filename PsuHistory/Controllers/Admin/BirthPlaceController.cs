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
    public class BirthPlaceController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseBusinessService<Guid, BirthPlace> birthPlaceService;

        public BirthPlaceController(
            IMapper mapper, 
            IBaseBusinessService<Guid, BirthPlace> birthPlaceService)
        {
            this.mapper = mapper;
            this.birthPlaceService = birthPlaceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
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
        public async Task<IActionResult> PostAsync([FromBody] CreateBirthPlace createBirthPlace)
        {
            var birthPlace = mapper.Map<CreateBirthPlace, BirthPlace>(createBirthPlace);

            var validation = await birthPlaceService.InsertAsync(birthPlace);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateBirthPlace updateBirthPlace)
        {
            var birthPlace = mapper.Map<UpdateBirthPlace, BirthPlace>(updateBirthPlace);

            var validation = await birthPlaceService.UpdateAsync(birthPlace);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await birthPlaceService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
