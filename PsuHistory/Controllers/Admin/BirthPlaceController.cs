using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class BirthPlaceController : ControllerBase
    {
    //        IService<BirthPlace> _service;
    //
    //        public BirthPlaceController(IService<BirthPlace> service)
    //        {
    //            _service = service;
    //        }
    //
    //        [HttpGet]
    //        public async Task<IActionResult> GetAll()
    //        {
    //            var birthPlaces = await _service.GetAllAsync();
    //            return Ok(birthPlaces);
    //        }
    //
    //        [HttpGet]
    //        [Route("{id}")]
    //        public async Task<IActionResult> Get([FromRoute] int id)
    //        {
    //            var birthPlace = await _service.GetAsync(id);
    //
    //            if (birthPlace is null)
    //            {
    //                ModelState.AddModelError("Error", "Не существует такого ID");
    //            }
    //
    //            if (!ModelState.IsValid)
    //            {
    //                return BadRequest(ModelState);
    //            }
    //
    //            return Ok(birthPlace);
    //        }
    //
    //        [HttpPost]
    //        [Route("add")]
    //        public async Task<IActionResult> Add([FromBody] BirthPlace birthPlace)
    //        {
    //            if (!await _service.CreateAsync(birthPlace))
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
    //        public async Task<IActionResult> Edit([FromBody] BirthPlace birthPlace, [FromRoute] int id)
    //        {
    //            birthPlace.Id = id;
    //
    //            if ((await _service.GetAsync(id)) is null)
    //            {
    //                ModelState.AddModelError("Error", "Не существует такого ID");
    //            } 
    //            else if (!await _service.UpdateAsync(birthPlace))
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
