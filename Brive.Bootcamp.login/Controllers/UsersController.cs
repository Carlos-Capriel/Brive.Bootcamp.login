using Brive.Bootcamp.login.Models;
using Brive.Bootcamp.login.Services;
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
        private readonly IUsers _users;
        public UsersController(IUsers users)
        {
            _users = users;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UsersTest user)
        {
            if (user.Email == null || user.Password == null)
            {
                return BadRequest(new { status = 400, information = "Missing email or password"});
            }

            // TODO Validar que el registro exista en la BD
            if (!user.Email.Equals("Carlos") || !user.Password.Equals("CaprielG"))
            {
                return BadRequest(new { status = 400, informaton = "Incorrect Email or password" });
            }

            return Ok(new { status = 202, Email = user.Email, Password = user.Password });
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { Users = _users.getUser(), status = 200 });
        }
    }
}
