using Microsoft.AspNetCore.Mvc;
using PsuHistory.Business.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsuHistory.Controllers.Abstraction
{
    public class AbstractionControllerBase : ControllerBase
    {
        public ObjectResult CreateObjectResult<TResult>(ValidationModel<TResult> validation)
        {
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors });

            return Ok(validation.Result);
        }
    }
}
