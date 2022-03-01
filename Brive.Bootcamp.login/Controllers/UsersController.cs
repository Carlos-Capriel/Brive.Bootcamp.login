using Brive.Bootcamp.login.Models;
using Brive.Bootcamp.login.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Brive.Bootcamp.login.Controllers
{
    [EnableCors("Login")]
    [Route("login/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGlobalUtilities _utilities;
        public UsersController(IGlobalUtilities utilities)
        {
            _utilities = utilities;
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsersAccount user)
        {
            if (user.Email == null || user.Password == null 
                    || user.Email == "" || user.Password == "")
            {
                return BadRequest(new { status = 400, information = "Missing email or password"});
            }

            if (!_utilities.verifyAccount(user.Email, user.Password))
            {
                return NotFound(new { status = 404, information = "Incorrect email or password"});
            }

            return Accepted(new { status = 202, Email = user.Email, Password = user.Password});
        }

        [HttpPost("register")]
        public ActionResult Post([FromBody]Users user)
        {
            bool result = _utilities.SaveUser(user);
            if (result)
            {
                return Created("/login/Users/register", new { status = 201, information = "Done" });
            }

            return BadRequest(new { status = 400, information = "Exist" });
        }

        [HttpGet("{password}")]
        public ActionResult Get(string password)
        {
            return Ok(new {status = 200 , hash = _utilities.hashPassword(password) });
        }
    }
}
