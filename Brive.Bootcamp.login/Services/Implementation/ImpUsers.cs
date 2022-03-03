﻿using Brive.Bootcamp.login.DBContext;
using Brive.Bootcamp.login.Models;
using System;
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

        public bool userExist(string email, string password)
        {
            var exist = _context.Users.Where(b => b.Email == email && b.Password == password);
            if (!(exist.Count() > 0))
            {
                return false;
            }

            return true;
        }

        public string GetUserName(string email)
        {
            var registro = _context.Users.Where(b => b.Email == email).Select(n => n.Name);

            string[] valor = registro.ToArray();

            return valor[0];
        }

        public async void SaveUser(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
