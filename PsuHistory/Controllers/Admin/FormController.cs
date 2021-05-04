using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
//        IUnitOfWork _database;
//        IService<Form> _service;
//        IWebHostEnvironment _appEnvironment;
//
//        public FormController(IService<Form> service, IUnitOfWork database, IWebHostEnvironment appEnvironment)
//        {
//            _service = service;
//            _database = database;
//            _appEnvironment = appEnvironment;
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var forms = await _service.GetAllAsync();
//            return Ok(forms);
//        }
//
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var form = await _service.GetAsync(id);
//            //ViewBag.Attachment = (await _database.AttachmentForms.GetAllAsync()).Where(a => a.FormId == id);
//            return Ok(form);
//        }
//
//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> Add([FromBody] Form form, [FromForm] IFormFileCollection uploads)
//        {
//            if (!await _service.CreateAsync(form))
//            {
//                ModelState.AddModelError("", "Уже существует");
//
//                return BadRequest(form);
//            }
//
//            var id = (await _service.FindAsync(form)).Id;
//            string path = "/files/save/form/";
//
//            foreach (var uploadedFile in uploads)
//            {
//                string fileName = Guid.NewGuid() + "." + uploadedFile.ContentType.Split("/")[1];
//
//                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path + fileName, FileMode.Create))
//                {
//                    await uploadedFile.CopyToAsync(fileStream);
//                    await _database.AttachmentForms.CreateAsync(new AttachmentForm() {
//                        Path = path + fileName,
//                        FileType = uploadedFile.ContentType.Split("/")[1],
//                        FormId = id
//                    });
//                }
//            }
//
//            return Ok();
//        }
//
//        [HttpPut]
//        [Route("edit/{id}")]
//        public async Task<IActionResult> Edit([FromBody] Form form, [FromForm] IFormFileCollection uploads, [FromRoute] int id)
//        {
//            form.Id = id;
//
//            if (!await _service.UpdateAsync(form))
//            {
//                ModelState.AddModelError("", "Уже существует");
//
//                //ViewBag.Attachment = (await _database.AttachmentForms.GetAllAsync()).Where(a => a.FormId == id);
//
//                return BadRequest(form);
//            }
//
//            string path = "/files/save/form/";
//
//            foreach (var uploadedFile in uploads)
//            {
//                string fileName = Guid.NewGuid() + "." + uploadedFile.ContentType.Split("/")[1];
//
//                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path + fileName, FileMode.Create))
//                {
//                    await uploadedFile.CopyToAsync(fileStream);
//                    await _database.AttachmentForms.CreateAsync(new AttachmentForm()
//                    {
//                        Path = path + fileName,
//                        FileType = uploadedFile.ContentType.Split("/")[1],
//                        FormId = id
//                    });
//                }
//            }
//
//            return Ok("Form");
//        }
//
//        [HttpDelete]
//        [Route("delete/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] int id)
//        {
//            await _service.DeleteAsync(id);
//            return Ok();
//        }
    }
}
