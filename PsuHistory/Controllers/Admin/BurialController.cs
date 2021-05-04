using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BurialController : ControllerBase
    {
//        IUnitOfWork _database;
//        IService<Burial> _service;
//        IWebHostEnvironment _appEnvironment;
//
//        public BurialController(IService<Burial> service, IUnitOfWork database, IWebHostEnvironment appEnvironment)
//        {
//            _service = service;
//            _database = database;
//            _appEnvironment = appEnvironment;
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var burials = await _service.GetAllAsync();
//            return Ok(burials);
//        }
//
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var burial = await _service.GetAsync(id);
//
//            if (burial is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            //ViewBag.Attachment = (await _database.AttachmentBurials.GetAllAsync()).Where(a => a.BurialId == id);
//            return Ok(burial);
//        }
//
//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> Add([FromBody] Burial burial)
//        {
//            if (!await _service.CreateAsync(burial))
//            {
//                ModelState.AddModelError("Error", "Не существует Захоронения с таким ID");
//            }
//
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok();
//        }
//
//        [HttpPut]
//        [Route("edit/{id}")]
//        public async Task<IActionResult> Edit([FromBody] Burial burial, [FromRoute] int id)
//        {
//            burial.Id = id;
//
//            if ((await _service.GetAsync(id)) is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            else if (!await _service.CreateAsync(burial))
//            {
//                ModelState.AddModelError("Error", "Не существует Захоронения с таким ID");
//            }
//
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok();
//        }
//
//        [HttpDelete]
//        [Route("delete/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] int id)
//        {
//            if (!await _service.DeleteAsync(id))
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok();
//        }
    }
}
