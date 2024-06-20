using CoreService.Interface;
using SharedLibrary.Model;
using CoreService.Repository;
using CoreService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace CoreService
{
    public class AuthenticationService :  IAuthentication
    {
        private static Dictionary<string, User> authenticatedUsers =
            new Dictionary<string, User>();
        
        public bool Registration(string username, string password)
        {
            using (var db = new UserContextDB())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Username == username);
                if (existingUser != null)
                {
                    return false; 
                }

                string encryptedPassword = EncryptData(password);
                User user = new User(username, encryptedPassword);
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;

        }

        public string Login(string username, string password)
        {
            using (var db = new UserContextDB())
            {
                foreach (var user in db.Users)
                {
                    if (username == user.Username &&
                        ValidateEncryptedData(password, user.Password))
                    {
                        string token = GenerateToken(username);
                        authenticatedUsers.Add(token, user);
                        return token;
                    }
                }
            }
            return "Login failed";

        }

        public bool Logout(string token)
        {
            return authenticatedUsers.Remove(token);

        }

        private static string EncryptData(string valueToEncrypt)
        {
            string GenerateSalt()
            {
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                byte[] salt = new byte[32];
                crypto.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
            string EncryptValue(string strValue)
            {
                string saltValue = GenerateSalt();
                byte[] saltedPassword = Encoding.UTF8.GetBytes(saltValue + strValue);
                using (SHA256Managed sha = new SHA256Managed())
                {
                    byte[] hash = sha.ComputeHash(saltedPassword);
                    return $"{Convert.ToBase64String(hash)}:{saltValue}";
                }
            }
            return EncryptValue(valueToEncrypt);
        }

        private static bool ValidateEncryptedData(string valueToValidate,
            string valueFromDatabase)
        {
            string[] arrValues = valueFromDatabase.Split(':');
            string encryptedDbValue = arrValues[0];
            string salt = arrValues[1];
            byte[] saltedValue = Encoding.UTF8.GetBytes(salt + valueToValidate);
            using (var sha = new SHA256Managed())
            {
                byte[] hash = sha.ComputeHash(saltedValue);
                string enteredValueToValidate = Convert.ToBase64String(hash);
                return encryptedDbValue.Equals(enteredValueToValidate);
            }
        }
        private string GenerateToken(string username)
        {
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] randVal = new byte[32];
            crypto.GetBytes(randVal);
            string randStr = Convert.ToBase64String(randVal);
            return username + randStr;
        }

        public bool IsUserAuthenticated(string token)
        {
            return authenticatedUsers.ContainsKey(token);
        }
    }
}
