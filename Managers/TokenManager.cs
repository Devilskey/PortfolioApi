using System.Security.Cryptography;
using webApi.Types;

namespace webApi.Managers
{
    public class TokenManager 
    {
        protected static readonly string key = "token";
        public static void TokenGenerator() 
        {
            TokenType.time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            TokenType.key = Guid.NewGuid().ToByteArray();
            TokenType.token = Convert.ToBase64String(TokenType.time.Concat(TokenType.key).ToArray());

        }
    }
}
