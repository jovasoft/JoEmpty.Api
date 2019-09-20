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
            if (user == null) return NotFound(new { status = "error", message = "user not found." });

            UserModel userModel = new UserModel { FirstName = user.FirstName, LastName = user.LastName, Mail = user.Mail };
            return Ok(userModel);
        }

        // PUT: api/User
        [HttpPut]
        public IActionResult Put([FromBody] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                User user = userService.Get(User.Identity.Name);
                if (user == null) return NotFound(new { status = "error", message = "user not found." });

                bool isUpdated = false;

                if (!string.IsNullOrEmpty(userModel.FirstName) && userModel.FirstName != user.FirstName) { user.FirstName = userModel.FirstName; isUpdated = true; }
                if (!string.IsNullOrEmpty(userModel.LastName) && userModel.LastName != user.LastName) { user.LastName = userModel.LastName; isUpdated = true; }

                if (isUpdated) userService.Update(user);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // POST: api/user/changepassword

        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                User user = userService.Get(User.Identity.Name);
                if (user == null) return NotFound(new { status = "error", message = "user not found." });

                changePasswordModel.OldPassword = Core.Helpers.Encryption.Calculate(changePasswordModel.OldPassword);
                changePasswordModel.NewPassword = Core.Helpers.Encryption.Calculate(changePasswordModel.NewPassword);

                bool isChanged = userService.ChangePassword(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);

                if (isChanged)
                    return Ok(new { status = "success" });
                else
                    return Ok(new { status = "failed", message = "Wrong old password." });
            }

            return BadRequest();
        }

        // DELETE: api/User
        [HttpDelete]
        public IActionResult Delete([FromBody] string password)
        {
            User user = userService.Get(User.Identity.Name);
            if (user == null) return NotFound(new { status = "error", message = "user not found." });

            if (user.Password == Core.Helpers.Encryption.Calculate(password))
            {
                userService.Delete(user);
                return Ok(new { status = "success" });
            }
            else
                return Ok(new { status = "failed", message = "wrong password." });
        }
    }
}
