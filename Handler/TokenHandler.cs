using System.Security.Cryptography;
using webApi.Types;

namespace webApi.Managers;

public class TokenHandler 
{
    protected static readonly string key = "253f346@sf4";
    public static void TokenGenerator() 
    {
        Token.time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        Token.key = Guid.NewGuid().ToByteArray();
        Token.token = Convert.ToBase64String(Token.time.Concat(Token.key).ToArray());
        Token.Creation = DateTime.Now;
        Token.Expire = DateTime.Now.AddHours(4);
    }
}
