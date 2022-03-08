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
                    || userAccount.Email == "" || userAccount.Password == "" || !_utilities.VerifyEmail(userAccount.Email))
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
                        _utilities.SaveUser(user) ? // hasheo en SaveUser
                        Created("/login/Users/register", new { status = 201, information = "Done" }) 
                        :
                        BadRequest(_utilities.messageResponse(400, "Email ya registrado"));
                }

                return BadRequest(_utilities.messageResponse(400, "Password invalido"));
            }

            return BadRequest(_utilities.messageResponse(400, "Email invalido"));
        }

        [HttpPost("password")]
        public ActionResult Post(string password, string confirmPassword)
        {
            return Ok(new {status = 200 , hash = _utilities.hashPassword(password) });
        }

        [HttpPut("update")]
        public IActionResult Put([FromBody]UserUpdatePassword user)
        {
            if (_utilities.EmailExist(user.Email))
            {
                if (user.Password == user.ConfirmPassword)
                {
                    if (_utilities.PasswordNotTheSame(user.Email, _utilities.hashPassword(user.Password)) && _utilities.VerifyPassword(user.Password))
                    {

                        _utilities.UpdatePassword(user.Email, user.Password);
                        return Ok(_utilities.messageResponse(200, "Updated password")) ;
                    }

                    return BadRequest(_utilities.messageResponse(400, "Wrong password")); // el password nuevo es el mismo al viejo
                }

                return BadRequest(_utilities.messageResponse(400, "Not match password")); // las contraseñas no son iguales
            }

            return NotFound(_utilities.messageResponse(404, "Email not found"));
        }
    }
}
