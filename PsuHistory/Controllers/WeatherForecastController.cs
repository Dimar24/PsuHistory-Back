using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsuHistory.API.Host.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IBaseService<Guid, TypeBurial> baseService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBaseService<Guid, TypeBurial> baseService)
        {
            _logger = logger;
            this.baseService = baseService;
        }

       // [HttpGet]
       // public async Task<IActionResult> Get()
       // {
       //     var rng = new Random();
       //     return await Enumerable.Range(1, 5).Select(index => new WeatherForecast
       //     {
       //         Date = DateTime.Now.AddDays(index),
       //         TemperatureC = rng.Next(-20, 55),
       //         Summary = Summaries[rng.Next(Summaries.Length)]
       //     })
       //     .ToArray();
       // }

        [HttpGet]
        public async Task<IActionResult> GetTypeList()
        {
            var typeList = await baseService.GetAllAsync();
            return Ok(typeList);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var type = new TypeBurial()
            {
                Name = "Тип № Один",
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now.AddDays(-5)
            };

            await baseService.InsertAsync(type);

            return Ok();
        }
    }
}
