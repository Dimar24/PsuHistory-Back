using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class VictimController : ControllerBase
    {
//        IService<Victim> _service;
//
//        public VictimController(IService<Victim> service)
//        {
//            _service = service;
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var victims = await _service.GetAllAsync();
//            return Ok(victims);
//        }
//
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var victim = await _service.GetAsync(id);
//
//            if (victim is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok(victim);
//        }
//
//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> Add([FromBody] Victim victim)
//        {
//            if (!await _service.CreateAsync(victim))
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
//        public async Task<IActionResult> Edit([FromBody] Victim victim, [FromRoute] int id)
//        {
//            victim.Id = id;
//
//            if ((await _service.GetAsync(id)) is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            else if (!await _service.UpdateAsync(victim))
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
