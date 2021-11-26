using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using weekly.Models;
using weekly.Services;

namespace weekly.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IJwtAuth _jwtAuth;
        public UserController(UserService userService, IJwtAuth jwtAuth)
        {
            _userService = userService;
            _jwtAuth = jwtAuth;
        }
        [HttpGet]
        public ActionResult<List<User>> Get() =>
           _userService.Get();
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);
            var token = _jwtAuth.Authentication(user.Email, user.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            return CreatedAtRoute("GetUser", token);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return NoContent();
        }

        [AllowAnonymous]   
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            User user = _userService.Verify(userCredential.UserName,userCredential.Password);
            if(user == null)
            {
                return Unauthorized("Bad credentials: " + userCredential.UserName + ".");
            }
            var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
