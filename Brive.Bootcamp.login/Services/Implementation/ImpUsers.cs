using Brive.Bootcamp.login.DBContext;
using Brive.Bootcamp.login.Models;
using System.Linq;

namespace Brive.Bootcamp.login.Services.Implementation
{
    public class ImpUsers : IUsers
    {
        private readonly ProjectContext _context;
        public ImpUsers(ProjectContext context)
        {
            _context = context;
        }
        public Users[] getUser()
        {
            return _context.Users.ToArray();
        }
    }
}
