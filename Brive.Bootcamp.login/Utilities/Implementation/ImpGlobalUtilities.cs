using Brive.Bootcamp.login.Models;
using Brive.Bootcamp.login.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Brive.Bootcamp.login.Utilities
{
    public class ImpGlobalUtilities : IGlobalUtilities
    {
        private IUsers _users;

        public ImpGlobalUtilities(IUsers users)
        {
            _users = users;
        }

        public bool SaveUser(Users user)
        {
            if (user == null || verifyAccount(user.Email, user.Password))
            {
                return false;
            }
            
            user.Password = hashPassword(user.Password);
            _users.SaveUser(user);

            return true;
        }

        public bool verifyAccount(string email, string password)
        {
            password = hashPassword(password);
            return _users.userExist(email, password);
        }
        
        public string hashPassword(string passwordU) 
        {
            byte[] salt = new byte[128 / 8];
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2 (
                    password: passwordU,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8 
                )
            );

            return hashed;
        }
    }
}
