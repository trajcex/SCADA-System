using CoreService.Interface;
using CoreService.Model;
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
    public class CoreService : ICore, IAuthentication
    {
        private static Dictionary<string, User> authenticatedUsers =
            new Dictionary<string, User>();
        public List<Tag> GetTags()
        {
            ITagRepository tagRepository = new TagRepository();
            return tagRepository.GetTags()["DigitalInputTag"];
        }

        public void SaveTags()
        {
            ITagRepository tagRepository = new TagRepository();
            List<Tag> di = new List<Tag>
            {
                new DigitalInputTag("DI1", "Digital Input Tag 1", "Address1", "Driver1", 1, true),
                new DigitalInputTag("DI2", "Digital Input Tag 2", "Address2", "Driver2", 1, false)
            };

                List<Tag> d0 = new List<Tag>
            {
                new DigitalOutputTag(100, "DO1", "Digital Output Tag 1", "Address1"),
                new DigitalOutputTag(200, "DO2", "Digital Output Tag 2", "Address2")
            };

                List<Tag> ai = new List<Tag>
            {
                new AnalogInputTag("AI1", "Analog Input Tag 1", "Address1", new List<Alarm>(), "Driver1", 1, true, "Volts", 0, 100),
                new AnalogInputTag("AI2", "Analog Input Tag 2", "Address2", new List<Alarm>(), "Driver2", 2, false, "Amps", 0, 50)
            };

                List<Tag> ao = new List<Tag>
            {
                new AnalogOutputTag("AO1", "Analog Output Tag 1", "Address1", 50, "Volts", 0, 100),
                new AnalogOutputTag("AO2", "Analog Output Tag 2", "Address2", 75, "Amps", 0, 50)
            };

            Dictionary<string, List<Tag>> map = new Dictionary<string, List<Tag>>();
            map.Add("DigitalInputTags", di);
            map.Add("DigitalOutputTags", d0);
            map.Add("AnalogInputTags", ai);
            map.Add("AnalogOutputTags", ao);
            tagRepository.SaveTags(map);
        }

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
