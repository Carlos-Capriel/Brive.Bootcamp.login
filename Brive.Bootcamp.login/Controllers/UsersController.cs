using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login.Controllers
{
    [Route("login/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> Get(string email, string password)
        {
            if (email == null || password == null)
            {
                return BadRequest();
            }
            return Ok(new { Email = email, Password =  password });
        }
    }
}
