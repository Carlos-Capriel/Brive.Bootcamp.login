using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login.Utilities
{
    public interface IGlobalUtilities
    {
        bool verifyUser(string email, string password);
        string hashPassword(string password);
    }
}
