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
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Mail) || string.IsNullOrEmpty(loginModel.Password)) return BadRequest();

            loginModel.Password = Core.Helpers.Encryption.Calculate(loginModel.Password);

            User user = authService.Login(loginModel.Mail, loginModel.Password);

            if (user == null) return BadRequest(new { message = "Mail or password is incorrect." });

            RegisterModel userModel = new RegisterModel { FirstName = user.FirstName, LastName = user.LastName, Mail = user.Mail, Token = user.Token };

            return Ok(userModel);
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (!userModel.Mail.Contains("@") || !userModel.Mail.Contains(".")) return BadRequest(new { message = "invalid mail address."});

                userModel.Password = Core.Helpers.Encryption.Calculate(userModel.Password);

                userModel.FirstName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.FirstName.Trim().ToLower());

                userModel.LastName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.LastName.Trim().ToLower());

                User user = new User { FirstName = userModel.FirstName, LastName = userModel.LastName, Mail = userModel.Mail, Password = userModel.Password };

                string token = authService.Register(user);

                if(string.IsNullOrEmpty(token)) return BadRequest(new { message = "User already registered." });

                userModel.Token = token;
                userModel.Password = null;

                return Ok(userModel);
            }
            else return BadRequest();
        }

        // POST: api/auth/logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
