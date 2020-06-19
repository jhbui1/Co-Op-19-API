using CoOp19API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CoOp19API.Domain
{
    public static class BCryptHash
    {
        public static bool VerifyUser(IEnumerable<UsersView> users,string pwd)
        {
            var user = users.First();
            if(BCrypt.Net.BCrypt.Verify(pwd+user.Salt,user.Password))
            {
                return true;
            }

            return false;

        }
        public static string[] HashPassword(string password)
        {
            var random = new RNGCryptoServiceProvider();
            int PASSWORD_BCRYPT_COST = 13;
            int salt_length = 32;
            byte[] PASSWORD_SALT_BYTES = new byte[salt_length];
            random.GetNonZeroBytes(PASSWORD_SALT_BYTES);
            string PASSWORD_SALT = Convert.ToBase64String(PASSWORD_SALT_BYTES);
            string salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
            return new string[] { BCrypt.Net.BCrypt.HashPassword(password, salt), salt };

        }
    }
}
