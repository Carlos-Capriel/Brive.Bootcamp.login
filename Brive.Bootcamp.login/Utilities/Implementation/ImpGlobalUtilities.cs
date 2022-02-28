﻿using Brive.Bootcamp.login.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login.Utilities
{
    public class ImpGlobalUtilities : IGlobalUtilities
    {
        private IUsers _users;

        public ImpGlobalUtilities(IUsers users)
        {
            _users = users;
        }

        public bool verifyUser(string email, string password)
        {
            return _users.userExist(email, password);
        }
        
        public string hashPassword(string passwordU) 
        {
            byte[] salt = new byte[128 / 8];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
             password: passwordU,
             salt: salt,
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 100000,
             numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}