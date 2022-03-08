using Brive.Bootcamp.login.Models;

namespace Brive.Bootcamp.login.Services
{
    public interface IUsers
    {
        Users[] getUser();
        bool userExist(string email, string password);
        bool UpdatePassword(string email, string newPassword);
        bool PasswordNotTheSame(string email, string newPassword);
        string GetUserName(string email);
        void SaveUser(Users user);
        bool EmailExist(string email);
    }

}
