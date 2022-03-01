using Brive.Bootcamp.login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login.Utilities
{
    public interface IGlobalUtilities
    {
        bool SaveUser(Users user);
        bool verifyUser(string email, string password);
        string hashPassword(string password);
    }
}
