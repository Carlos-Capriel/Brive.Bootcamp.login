using Brive.Bootcamp.login.Models;

namespace Brive.Bootcamp.login.Utilities
{
    public interface IGlobalUtilities
    {
        bool SaveUser(Users user);
        bool verifyAccount(string email, string password);
        string hashPassword(string password);
    }
}
