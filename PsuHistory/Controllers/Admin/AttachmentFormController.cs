using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Controllers.Abstraction;
using PsuHistory.Data.Domain.Models.Histories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PsuHistory.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class AttachmentFormController : AbstractionControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseService<Guid, AttachmentForm> attachmentFormService;

        public AttachmentFormController(
            IMapper mapper,
            IBaseService<Guid, AttachmentForm> attachmentFormService)
        {
            this.mapper = mapper;
            this.attachmentFormService = attachmentFormService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var validation = await attachmentFormService.GetAsync(id);

            return CreateObjectResult(validation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var validation = await attachmentFormService.GetAllAsync();

            return CreateObjectResult(validation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CreateAttachmentForm createAttachmentForm)
        {
            var attachmentForm = mapper.Map<AttachmentForm>(createAttachmentForm);

            var validation = await attachmentFormService.InsertAsync(attachmentForm);

            return CreateObjectResult(validation);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] UpdateAttachmentForm updateAttachmentForm)
        {
            var attachmentForm = mapper.Map<AttachmentForm>(updateAttachmentForm);

            var validation = await attachmentFormService.UpdateAsync(attachmentForm);

            return CreateObjectResult(validation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var validation = await attachmentFormService.DeleteAsync(id);

            return CreateObjectResult(validation);
        }
    }
}
