using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin

{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class TypeVictimController : ControllerBase
    {
//        IService<TypeVictim> _service;
//
//        public TypeVictimController(IService<TypeVictim> service)
//        {
//            _service = service;
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var typeVictims = await _service.GetAllAsync();
//            return Ok(typeVictims);
//        }
//
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var typeVictim = await _service.GetAsync(id);
//
//            if (typeVictim is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok(typeVictim);
//        }
//
//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> Add([FromBody] TypeVictim typeVictim)
//        {
//            if (!await _service.CreateAsync(typeVictim))
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
//        public async Task<IActionResult> Edit([FromBody] TypeVictim typeVictim, [FromRoute] int id)
//        {
//            typeVictim.Id = id;
//
//            if ((await _service.GetAsync(id)) is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            else if(!await _service.UpdateAsync(typeVictim))
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
