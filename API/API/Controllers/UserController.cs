using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            User user = userService.Get(User.Identity.Name);
            UserModel userModel = new UserModel { FirstName = user.FirstName, LastName = user.LastName, Mail = user.Mail };
            return Ok(userModel);
        }

        // PUT: api/User
        [HttpPut]
        public IActionResult Put([FromBody] UserModel userModel)
        {
            User user = userService.Get(User.Identity.Name);

            if (string.IsNullOrEmpty(userModel.FirstName)) user.FirstName = userModel.FirstName;
            if (string.IsNullOrEmpty(userModel.LastName)) user.LastName = userModel.LastName;
            if (string.IsNullOrEmpty(userModel.Password)) user.Password = userModel.Password;
            if (string.IsNullOrEmpty(userModel.Mail)) user.Mail = userModel.Mail;

            userService.Update(user);

            return Ok(new { status = "success" });
        }

        // DELETE: api/User
        [HttpDelete]
        public IActionResult Delete()
        {
            User user = userService.Get(User.Identity.Name);
            userService.Delete(user);

            return Ok(new { status = "success" });
        }
    }
}
