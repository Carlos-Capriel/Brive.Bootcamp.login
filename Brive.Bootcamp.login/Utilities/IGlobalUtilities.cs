using Brive.Bootcamp.login.Models;

namespace Brive.Bootcamp.login.Utilities
{
    public interface IGlobalUtilities
    {
        bool SaveUser(Users user);
        bool verifyAccount(string email, string password);
        bool VerifyEmail(string email);
        bool EmailExist(string email);
        string hashPassword(string password);
        bool VerifyPassword(string password);
        bool PasswordNotTheSame(string email, string password);

        int UpdatePassword(string email, string password);
        string GetUserName(string email);
        object messageResponse(int status, string info);
        object messageResponse(int status, string info, string key, string value);
        // string Token(string email);
    }
}
