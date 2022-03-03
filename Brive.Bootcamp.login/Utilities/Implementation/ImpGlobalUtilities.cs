using Brive.Bootcamp.login.Models;
using Brive.Bootcamp.login.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
            if (user == null || EmailExist(user.Email))
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

        public bool VerifyEmail(string email)
        {
            if ((new EmailAddressAttribute().IsValid(email))) {
                return true;
            }
            return false;
        }

        public bool EmailExist(string email)
        {
            return _users.EmailExist(email);
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

        public bool VerifyPassword(string password)
        {
            Regex regex = new Regex("^(?=.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[:word:]).*$");
            if (regex.IsMatch(password))
            {
                return true;
            }
            return false;
        }

        public string GetUserName(string email)
        {
            return _users.GetUserName(email);
        }

        public object messageResponse(int status, string info)
        {
            return new
            {
                status = status,
                information = info
            };
        }
        public object messageResponse(int status, string info, string key, string value)
        {
            return new
            {
                status = status,
                information = info,
                key = value
            };
        }
    }
}
