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
        public IActionResult Post([FromBody]UsersAccount userAccount)
        {
            if (userAccount.Email == null || userAccount.Password == null 
                    || userAccount.Email == "" || userAccount.Password == "")
            {
                return BadRequest(_utilities.messageResponse(400, "Missing something"));
            }

            if (!_utilities.verifyAccount(userAccount.Email, userAccount.Password))
            {         
                return NotFound(_utilities.messageResponse(404, "Incorrect email or password"));
            }

            return Accepted(_utilities.messageResponse(202, "Accepted", "UserName", _utilities.GetUserName(userAccount.Email)));
        }

        [HttpPost("register")]
        public ActionResult Post([FromBody]Users user)
        {
            if(user.Email == "" || user.Email == null || user.LastNameM == "" || user.LastNameM == null || user.LastNameP == ""
                || user.LastNameP == null || user.Password == "" || user.Password == null)
            {
              
                return BadRequest(_utilities.messageResponse(400, "Some is missing"));
            }

            if (_utilities.VerifyEmail(user.Email))
            {
                if (_utilities.VerifyPassword(user.Password))
                {
                    return 
                        _utilities.SaveUser(user) ? 
                        Created("/login/Users/register", new { status = 201, information = "Done" }) 
                        :
                        BadRequest(_utilities.messageResponse(400, "Email ya registrado"));
                }

                return BadRequest(_utilities.messageResponse(400, "Invalid password"));
            }

            return BadRequest(_utilities.messageResponse(400, "Invalid email"));
        }

        [HttpGet("{password}")]
        public ActionResult Get(string password)
        {
            return Ok(new {status = 200 , hash = _utilities.hashPassword(password) });
        }
    }
}
