using System.Security.Cryptography;
using System.Text;
using System;

namespace webApi.Security
{
    public static class EncryptionHandler
    {
        public static string StringHashPassword(string password){

            SHA256 sha = SHA256.Create();

            byte[] passwordByteArray = Encoding.Default.GetBytes(password);
            
            var HashedPassword = sha.ComputeHash(passwordByteArray);

            return Convert.ToBase64String(HashedPassword);
        }
    }
}
