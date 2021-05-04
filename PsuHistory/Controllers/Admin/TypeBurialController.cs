using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class TypeBurialController : ControllerBase
    {
//        IService<TypeBurial> _service;
//
//        public TypeBurialController(IService<TypeBurial> service)
//        {
//            _service = service;
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var typeBurials = await _service.GetAllAsync();
//            return Ok(typeBurials);
//        }
//
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var typeBurial = await _service.GetAsync(id);
//
//            if (typeBurial is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok(typeBurial);
//        }
//
//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> Add([FromBody] TypeBurial typeBurial)
//        {
//            if (!await _service.CreateAsync(typeBurial))
//            {
//                ModelState.AddModelError("Error", "Уже существует");
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
//        public async Task<IActionResult> Edit([FromBody] TypeBurial typeBurial, [FromRoute] int id)
//        {
//            typeBurial.Id = id;
//
//            if ((await _service.GetAsync(id)) is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            else if(!await _service.UpdateAsync(typeBurial))
//            {
//                ModelState.AddModelError("Error", "Уже существует элемент с данными значениями");
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
