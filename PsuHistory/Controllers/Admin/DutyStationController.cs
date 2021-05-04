using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PsuHistory.API.Host.Controllers.Admin
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DutyStationController : ControllerBase
    {
        //        IService<DutyStation> _service;
        //
        //        public DutyStationController(IService<DutyStation> service)
        //        {
        //            _service = service;
        //        }
        //
        //        [HttpGet]
        //        public async Task<IActionResult> GetAll()
        //        {
        //            var dutyStations = await _service.GetAllAsync();
        //            return BadRequest(dutyStations);
        //        }
        //
        //        [HttpGet]
        //        [Route("{id}")]
        //        public async Task<IActionResult> Get([FromRoute] int id)
        //        {
        //            var dutyStation = await _service.GetAsync(id);
        //
        //            if (dutyStation is null)
        //            {
        //                ModelState.AddModelError("Error", "Не существует такого ID");
        //            }
        //            
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }
        //
        //            return Ok(dutyStation);
        //        }
        //
        //        [HttpPost]
        //        [Route("add")]
        //        public async Task<IActionResult> Add([FromBody] DutyStation dutyStation)
        //        {
        //            if (!await _service.CreateAsync(dutyStation))
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
        //        public async Task<IActionResult> Edit([FromBody] DutyStation dutyStation, [FromRoute] int id)
        //        {
        //            dutyStation.Id = id;
        //
        //            if ((await _service.GetAsync(id)) is null)
        //            {
        //                ModelState.AddModelError("Error", "Не существует такого ID");
        //            }
        //            else if(!await _service.UpdateAsync(dutyStation))
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
