using Microsoft.AspNetCore.Mvc;
using PsuHistory.Common.Models;

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
