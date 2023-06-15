namespace webApi.Types;

public class Token
{
    public static byte[] time;
    public static byte[] key;
    public static string token;
    public static DateTime Creation;
    public static DateTime Expire;

    public static bool isExpired()
    {
        if(DateTime.Now > Expire)
        {
            token = "";
            return true;
        }
        return false;
    }
}
