using Microsoft.AspNetCore.Mvc;
using webApi.Managers;
using webApi.Types;
using webApi.Security;
using MySql.Data.MySqlClient;

namespace webApi.Controllers;

[ApiController]
public class AdminApi : ControllerBase
{
    [Route("CreateNewAdmin")]
    [HttpPost]
    public string CreateNewAdmin(Admin admin)
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

        using (DatabaseMysqlHandler databaseManager = new()){
            databaseManager.EditDatabase(mysqlCommand);
        }

        return "inserted into";
    }

    [Route("DeleteAdmin")]
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
    
        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler()){
            databaseManager.EditDatabase(mysqlCommand);
        }
        return "Admin Deleted";
    }

    [Route("EditAdmin")]
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

        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler()){
            databaseManager.EditDatabase(mysqlCommand);
        }
        return "admin Updated";
    }

    [Route("GetListAdmin")]
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

        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
        {
            json = databaseManager.Select(mySqlCommand);
        }
       return json;
    }
}
