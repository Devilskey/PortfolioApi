using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;
using webApi.Security;
using MySql.Data.MySqlClient;

namespace webApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AppLogin : ControllerBase
{
    private static ILogger Logger;
    public AppLogin(ILogger _logger)
    {
        Logger = _logger;
    }

    public static AdminData LoginData = new AdminData();

    [HttpPost]
    public string LoginCommand(AdminData adminLoginData)
    {
        Logger.Log(LogLevel.Debug, "TestMassage");

        string query = $"SELECT AdminName, AdminPassword FROM admin WHERE AdminName=@AdminName;";
        string json = string.Empty;
        
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@AdminName", adminLoginData.AdminName);

        using (DatabaseMysqlHandler databaseManger = new DatabaseMysqlHandler())
        {
            json = databaseManger.Select(mysqlCommand);
            json = json.Replace("[", "");
            json = json.Replace("]", "");
        }

        try
        {
           LoginData = JsonConvert.DeserializeObject<AdminData>(json);
        }
        catch(Exception ex) 
        { 
            return $"No Access granted: {ex}";
        }

        if (LoginData == null)
        { 
            return "User not found";
        }

        string hashedLoginPassword = EncryptionHandler.StringHashPassword(adminLoginData.AdminPassword);

        if (LoginData.AdminName != adminLoginData.AdminName || LoginData.AdminPassword != hashedLoginPassword)
        {
            return "no Access granted";
        }

        Console.WriteLine("Loged in");

        TokenHandler.TokenGenerator();

        return Token.token;
    }
}
