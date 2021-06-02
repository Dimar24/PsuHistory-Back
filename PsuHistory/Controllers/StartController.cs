using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsuHistory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StartController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(new { answer = "API working :)" });
		}

	}
}
