using Brive.Bootcamp.login.Helpers;
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

        public bool SaveUser(Users user) // en el controller se usan los metodos de validacion de email y password valido
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
            Regex regex = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
            if (regex.IsMatch(email)) {
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
            Regex regex = new Regex("^(?=.{8,}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?\\W)");
            if (regex.IsMatch(password))
            {
                return true;
            }
            return false;
        }

        public bool PasswordNotTheSame(string email, string password)
        {
            return _users.PasswordNotTheSame(email, password);
        }

        public int UpdatePassword(string email, string password)
        {
            password = hashPassword(password);
            _users.UpdatePassword(email, password);
            return 1;
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
