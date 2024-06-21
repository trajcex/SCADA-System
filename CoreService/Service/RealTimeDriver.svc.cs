using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using CoreService.Interface;

namespace CoreService
{
    public class RealTimeDriver : IRealTimeDriver
    {
        private static Dictionary<string ,int > rtuDicDictionary = new Dictionary<string ,int>();

        public bool ConnectRtu(string uid)
        {
            if (rtuDicDictionary.ContainsKey(uid))
            {
                return false;
            }
            else
            {
                rtuDicDictionary.Add(uid, 0);
                return true;
            }
        }

        public (bool Check, int value) SendRandomValue(string address, int random, byte[] m)
        {
            Receiver receiver = new Receiver();
            receiver.ImportPublicKey(address);
            bool check =  receiver.VerifySignedMessage(random.ToString(), m);
            if (check)
            { 
                rtuDicDictionary[address] = random;
            }
            return (check, rtuDicDictionary[address]);
            
        }

        public int GetValue(string address)
        {
            if (!rtuDicDictionary.ContainsKey(address)) { return 0; }
            return rtuDicDictionary[address];
        }

    }

    public class Receiver
    {
        private static CspParameters csp;
        private static RSACryptoServiceProvider rsa;
        const string IMPORT_FOLDER = @"C:\public_key\";
        const string PUBLIC_KEY_FILE = @"rsaPublicKey.txt";
        public void ImportPublicKey(string address)
        {
            string path = Path.Combine(IMPORT_FOLDER, address + "_" + PUBLIC_KEY_FILE);
            
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    csp = new CspParameters();
                    rsa = new RSACryptoServiceProvider(csp);
                    string publicKeyText = reader.ReadToEnd();
                    rsa.FromXmlString(publicKeyText);
                }
            }
        }
        public  bool VerifySignedMessage(string message, byte[] signature)
        {
            using (SHA256 sha = SHA256Managed.Create())
            {
                var hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message));
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                return deformatter.VerifySignature(hashValue, signature);
            }
        }
    }
}
