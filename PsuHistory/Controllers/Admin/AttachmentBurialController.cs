using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class AttachmentBurialController : ControllerBase
    {
    //    private IService<AttachmentBurial> _service;
    //    private IWebHostEnvironment _appEnvironment;
    //    private readonly IConfiguration _configuration;
    //
    //    public AttachmentBurialController(IService<AttachmentBurial> service, IWebHostEnvironment appEnvironment, IConfiguration configuration)
    //    {
    //        _service = service;
    //        _appEnvironment = appEnvironment;
    //        _configuration = configuration;
    //    }
    //
    //    [HttpGet]
    //    public async Task<IActionResult> GetAll()
    //    {
    //        var attachmentBurials = await _service.GetAllAsync();
    //        return Ok(attachmentBurials);
    //    }
    //
    //    [HttpGet]
    //    [Route("{id}")]
    //    public async Task<IActionResult> Get([FromRoute] int id)
    //    {
    //        var attachmentBurial = await _service.GetAsync(id);
    //
    //        if (attachmentBurial is null)
    //        {
    //            ModelState.AddModelError("Error", "Не существует такого ID");
    //        }
    //
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //
    //        return Ok(attachmentBurial);
    //    }
    //
    //    [HttpPost]
    //    [Route("add")]
    //    public async Task<IActionResult> Add([FromForm] AttachmentBurial attachmentBurial)// [FromBody] AttachmentBurial attachmentBurial,
    //    {
    //        if(attachmentBurial.FormFile is null)
    //        {
    //            ModelState.AddModelError("Error", "Загрузите файл");
    //        }
    //        else
    //        {
    //            attachmentBurial.FileType = Path.GetExtension(attachmentBurial.FormFile.FileName);
    //            attachmentBurial.FileName = Guid.NewGuid().ToString();
    //            attachmentBurial.FilePath = _appEnvironment.WebRootPath + "/Files/";
    //
    //            if (!_configuration.GetSection("FileFormat:RequiredAttachmentBurial").Value.Contains(attachmentBurial.FileType))
    //            {
    //                ModelState.AddModelError("Error", "Формат файла не подходит");
    //            }
    //            else if (!await _service.CreateAsync(attachmentBurial))
    //            {
    //                ModelState.AddModelError("Error", "Не существует Захоронения с таким ID");
    //            }
    //        }
    //        
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //
    //        return Ok();
    //    }
    //
    //    [HttpPut]
    //    [Route("edit/{id}")]
    //    public async Task<IActionResult> Edit([FromBody] AttachmentBurial attachmentBurial, [FromRoute] int id)
    //    {
    //        attachmentBurial.Id = id;
    //
    //        if ((await _service.GetAsync(id)) is null)
    //        {
    //            ModelState.AddModelError("Error", "Не существует такого ID");
    //        }
    //        else
    //        {
    //            if (attachmentBurial.FormFile is not null)
    //            {
    //                attachmentBurial.FileType = Path.GetExtension(attachmentBurial.FormFile.FileName);
    //                attachmentBurial.FileName = Guid.NewGuid().ToString();
    //                attachmentBurial.FilePath = _appEnvironment.WebRootPath + "/Files/";
    //
    //                if (!_configuration.GetSection("FileFormat:RequiredAttachmentBurial").Value.Contains(attachmentBurial.FileType))
    //                {
    //                    ModelState.AddModelError("Error", "Формат файла не подходит");
    //                }
    //            }
    //
    //            if (ModelState.IsValid && !await _service.CreateAsync(attachmentBurial))
    //            {
    //                ModelState.AddModelError("Error", "Не существует Захоронения с таким ID");
    //            }
    //        }
    //
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //
    //        return Ok();
    //    }
    //
    //    [HttpDelete]
    //    [Route("delete/{id}")]
    //    public async Task<IActionResult> Delete([FromRoute] int id)
    //    {
    //        if (!await _service.DeleteAsync(id))
    //        {
    //            ModelState.AddModelError("Error", "Не существует такого ID");
    //        }
    //
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }
    //
    //        return Ok();
    //    }
    }
}
