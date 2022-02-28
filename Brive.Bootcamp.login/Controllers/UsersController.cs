using Brive.Bootcamp.login.Models;
using Brive.Bootcamp.login.Services;
using Brive.Bootcamp.login.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post([FromBody]UsersTest user)
        {
            if (user.Email == null || user.Password == null 
                    || user.Email == "" || user.Password == "")
            {
                return BadRequest(new { status = 400, information = "Missing email or password"});
            }

            if (!_utilities.verifyUser(user.Email, user.Password))
            {
                return NotFound(new { status = 404, information = "Incorrect email or password"});
            }

            return Ok(new { status = 202, Email = user.Email, Password = user.Password});
        }

        [HttpGet("{password}")]
        public ActionResult Get(string password)
        {
            return Ok(new {status = 200 , hash = _utilities.hashPassword(password) });
        }
    }
}
