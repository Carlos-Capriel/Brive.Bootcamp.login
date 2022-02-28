using Brive.Bootcamp.login.Models;
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
    }
}
