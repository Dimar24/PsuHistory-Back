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
    public class AttachmentBurialController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, AttachmentBurial> attachmentBurialService;

        public AttachmentBurialController(
            IMapper mapper,
            IBaseService<Guid, AttachmentBurial> attachmentBurialService)
        {
            this.mapper = mapper;
            this.attachmentBurialService = attachmentBurialService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await attachmentBurialService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await attachmentBurialService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateAttachmentBurial createAttachmentBurial)
        {
            var attachmentBurial = mapper.Map<AttachmentBurial>(createAttachmentBurial);

            var validation = await attachmentBurialService.InsertAsync(attachmentBurial);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateAttachmentBurial updateAttachmentBurial)
        {
            var attachmentBurial = mapper.Map<AttachmentBurial>(updateAttachmentBurial);

            var validation = await attachmentBurialService.UpdateAsync(attachmentBurial);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await attachmentBurialService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
