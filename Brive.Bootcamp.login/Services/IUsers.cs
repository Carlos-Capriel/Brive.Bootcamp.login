using Brive.Bootcamp.login.Models;

namespace Brive.Bootcamp.login.Services
{
    public interface IUsers
    {
        Users[] getUser();
        bool userExist(string email, string password);
        void SaveUser(Users user);
    }

}
