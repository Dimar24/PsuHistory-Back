﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PsuHistory.API.Host.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class AttachmentBurialController : ControllerBase
    {
        // GET: api/<AttachmentForm>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AttachmentForm>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttachmentForm>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AttachmentForm>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AttachmentForm>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
