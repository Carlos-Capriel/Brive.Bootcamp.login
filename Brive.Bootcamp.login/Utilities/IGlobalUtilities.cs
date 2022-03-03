using Brive.Bootcamp.login.Models;

namespace Brive.Bootcamp.login.Utilities
{
    public interface IGlobalUtilities
    {
        bool SaveUser(Users user);
        bool verifyAccount(string email, string password);
        string hashPassword(string password);

        string GetUserName(string email);
        object messageResponse(int status, string info);
        object messageResponse(int status, string info, string key, string value);
    }
}
