using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        public IActionResult Success(string message = default(string), object data = default(object), int code = 200)
        {
            var response = new ResponseModel()
            {
                Success = true,
                Message = message,
                Data = data,
                Code = code
            };

            if (code == 201) return StatusCode(201, response);
            if (code == 202) return Accepted(response);
            if (code == 204) return NoContent();

            return Ok(response);
        }

        public IActionResult Error(string message = default(string), object data = default(object), int code = 400)
        {
            var response = new ResponseModel()
            {
                Success = false,
                Message = message,
                Data = data,
                Code = code
            };

            if (code == 403) return Forbid();
            if (code == 500) return StatusCode(500, null);
            if (code == 401) return Unauthorized(response);
            if (code == 404) return NotFound(response);

            return BadRequest(response);


        }
    }
}