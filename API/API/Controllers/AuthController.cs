using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] string mail, [FromBody] string password)
        {

            return Ok();
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel userModel)
        {

            User user = new User { FirstName = userModel.FirstName, LastName = userModel.LastName, Mail = userModel.Mail, Password = userModel.Password };

            string token = authService.Register(user);

            userModel.Token = token;

            return Ok(new { user = userModel });
        }

        // POST: api/auth/logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
