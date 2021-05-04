using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ConscriptionPlaceController : ControllerBase
    {
//        IService<ConscriptionPlace> _service;
//
//        public ConscriptionPlaceController(IService<ConscriptionPlace> service)
//        {
//            _service = service;
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var conscriptionPlaces = await _service.GetAllAsync();
//            return Ok(conscriptionPlaces);
//        }
//
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            var conscriptionPlace = await _service.GetAsync(id);
//
//            if (conscriptionPlace is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            
//            if(!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            return Ok(conscriptionPlace);
//        }
//
//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> Add([FromBody] ConscriptionPlace conscriptionPlace)
//        {
//            if (!await _service.CreateAsync(conscriptionPlace))
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
//        public async Task<IActionResult> Edit([FromBody] ConscriptionPlace conscriptionPlace, [FromRoute] int id)
//        {
//            conscriptionPlace.Id = id;
//
//            if ((await _service.GetAsync(id)) is null)
//            {
//                ModelState.AddModelError("Error", "Не существует такого ID");
//            }
//            else if(!await _service.UpdateAsync(conscriptionPlace))
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
