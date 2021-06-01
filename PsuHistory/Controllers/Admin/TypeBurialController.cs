using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.API.Controllers.Abstraction;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Threading.Tasks;

namespace PsuHistory.API.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin, Moderator")]
    [Route("api/admin/[controller]")]
    public class TypeBurialController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, TypeBurial> typeBurialService;

        public TypeBurialController(
            IMapper mapper,
            IBaseService<Guid, TypeBurial> typeBurialService)
        {
            this.mapper = mapper;
            this.typeBurialService = typeBurialService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await typeBurialService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await typeBurialService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateTypeBurial createTypeBurial)
        {
            var typeBurial = mapper.Map<TypeBurial>(createTypeBurial);

            var validation = await typeBurialService.InsertAsync(typeBurial);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateTypeBurial updateTypeBurial)
        {
            var typeBurial = mapper.Map<TypeBurial>(updateTypeBurial);

            var validation = await typeBurialService.UpdateAsync(typeBurial);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await typeBurialService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
