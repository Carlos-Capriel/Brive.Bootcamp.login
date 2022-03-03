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
                return BadRequest(_utilities.messageResponse(400, "Incorrect email or password"));
            }

            if (!_utilities.verifyAccount(user.Email, user.Password))
            {         
                return NotFound(_utilities.messageResponse(404, "Incorrect email or password"));
            }

            return Accepted(_utilities.messageResponse(202, "Accepted", "UserName", _utilities.GetUserName(user.Email)));
        }

        [HttpPost("register")]
        public ActionResult Post([FromBody]Users user)
        {
            bool result = _utilities.SaveUser(user);
            if (result)
            {
                return Created("/login/Users/register", new { status = 201, information = "Done" });
            }

            return BadRequest(_utilities.messageResponse(400, "Exist"));
        }

        [HttpGet("{password}")]
        public ActionResult Get(string password)
        {
            return Ok(new {status = 200 , hash = _utilities.hashPassword(password) });
        }
    }
}
