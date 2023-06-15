using Microsoft.AspNetCore.Mvc;
using webApi.Managers;
using webApi.Types;
using webApi.Security;
using MySql.Data.MySqlClient;

namespace webApi.Controllers;

[ApiController]
[Route("")]
public class AdminApi : ControllerBase
{
    [Route("AdminHandler")]
    [HttpPost]
    public string PostAdmin(Admin admin)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (admin.token != Token.token || Token.token == "")
            return "No Access";

        string HashedPassword = EncryptionHandler.StringHashPassword(admin.AdminPassword);
            
        string query = $"INSERT INTO `admin`(`AdminName`, `AdminPassword`, `AdminPhoneNumber`) VALUES (@AdminName, @AdminPassword, @AdminPhoneNumber);";
            
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@AdminName", admin.AdminName);
        mysqlCommand.Parameters.AddWithValue("@AdminPassword", HashedPassword);
        mysqlCommand.Parameters.AddWithValue("@AdminPhoneNumber", admin.AdminPhoneNumber);

        using (DatabaseHandler databaseManager = new()){
            databaseManager.EditDatabase(mysqlCommand);
        }
        return "inserted into";
    }

    [Route("AdminHandler")]
    [HttpDelete]
    public string DeleteAdmin(Admin admin)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (admin.token != Token.token || Token.token == "")
            return "No Access";

        string query = $"DELETE FROM `admin` WHERE `adminId`=@AdminId;";

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@AdminId", admin.AdminId);
    
        using (DatabaseHandler databaseManager = new DatabaseHandler()){
            databaseManager.EditDatabase(mysqlCommand);
        }
        return "Admin Deleted";
    }

    [Route("AdminHandler")]
    [HttpPut]
    public string PutAdmin(Admin admin)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (admin.token != Token.token || Token.token == "")
            return "No Access";

        string HashedPassword = EncryptionHandler.StringHashPassword(admin.AdminPassword);

        string query = $"UPDATE `admin` SET `AdminName`=@AdminName,`AdminPassword`=@AdminPassword,`AdminPhoneNumber`=@AdminPhoneNumber WHERE `AdminId`=@AdminId;";
            
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@AdminName", admin.AdminName);
        mysqlCommand.Parameters.AddWithValue("@AdminPassword", HashedPassword);
        mysqlCommand.Parameters.AddWithValue("@AdminPhoneNumber", admin.AdminPhoneNumber);
        mysqlCommand.Parameters.AddWithValue("@AdminId", admin.AdminId);

        using (DatabaseHandler databaseManager = new DatabaseHandler()){
            databaseManager.EditDatabase(mysqlCommand);
        }
        return "admin Updated";
    }

    [Route("AdminMenu")]
    [HttpPost]
    public string postMenuAdmin(Authentication auth)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (auth.token != Token.token || Token.token == "")
            return "No Access";
        string query = $"SELECT AdminId, AdminName FROM admin;";
        string json = string.Empty;
        MySqlCommand mySqlCommand = new MySqlCommand();
        mySqlCommand.CommandText = query;

        using (DatabaseHandler databaseManager = new DatabaseHandler())
        {
            json = databaseManager.Select(mySqlCommand);
        }
       return json;
    }
}
