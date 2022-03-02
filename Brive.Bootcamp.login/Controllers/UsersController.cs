using Brive.Bootcamp.login.Helpers;
using Brive.Bootcamp.login.Models;
using Brive.Bootcamp.login.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Brive.Bootcamp.login.Controllers
{
    [EnableCors("Login")]
    [Route("login/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGlobalUtilities _utilities;
        private readonly IConfiguration _conf;
        public UsersController(IGlobalUtilities utilities, IConfiguration config)
        {
            _conf = config;
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
            string secret = this._conf.GetValue<string>("Secret");
            var jwtHelper = new JWTHelper(secret);
            var token = jwtHelper.createToken(user.Email);

            return Accepted(_utilities.messageResponse(202, token));
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
