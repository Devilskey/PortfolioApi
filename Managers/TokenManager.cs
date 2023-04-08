using System.Security.Cryptography;
using webApi.Types;

namespace webApi.Managers
{
    public class TokenManager 
    {
        protected static readonly string key = "token";
        public static void TokenGenerator() 
        {
            OldToken.time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            OldToken.key = Guid.NewGuid().ToByteArray();
            OldToken.token = Convert.ToBase64String(OldToken.time.Concat(OldToken.key).ToArray());
        }
    }
}
