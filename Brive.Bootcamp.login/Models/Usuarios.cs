using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login.Models
{
    public class Usuarios
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool State { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
