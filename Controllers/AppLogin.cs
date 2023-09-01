using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;
using webApi.Security;
using MySql.Data.MySqlClient;

namespace webApi.Controllers;

[ApiController]
[Route("Login")]
public class AppLogin : ControllerBase
{
    private static ILogger Logger;
    public AppLogin(ILogger<AppLogin> _logger)
    {
        Logger = _logger;
    }

    public static AdminData[] LoginData = new AdminData[1];

    [HttpPost]
    public string LoginCommand(AdminData adminLoginData)
    {

        string query = $"SELECT AdminName, AdminPassword FROM admin WHERE AdminName=@AdminName;";
        string json = string.Empty;
        
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@AdminName", adminLoginData.AdminName);

        using (DatabaseMysqlHandler databaseManger = new DatabaseMysqlHandler())
        {
            json = databaseManger.Select(mysqlCommand);
        }

        try
        {
           LoginData = JsonConvert.DeserializeObject<AdminData[]>(json);
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

        if (LoginData[0].AdminName != adminLoginData.AdminName || LoginData[0].AdminPassword != hashedLoginPassword)
        {
            return "no Access granted";
        }

        Console.WriteLine("Loged in");

        TokenHandler.TokenGenerator();

        return Token.token;
    }
}
