﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "this page autherized.", "user 2" };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "user " + id;
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
