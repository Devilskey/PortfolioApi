using Microsoft.AspNetCore.Mvc;
using webApi.Managers;
using webApi.Types;
using webApi.Security;
using MySql.Data.MySqlClient;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminHandler : ControllerBase
    {
        [HttpPost]
        public string PostAdmin(AdminType admin)
        {
            if (admin.token == OldToken.token && OldToken.token != ""){

                string HashedPassword = EncryptionHandler.StringHashPassword(admin.AdminPassword);
                
                string query = $"INSERT INTO `admin`(`AdminName`, `AdminPassword`, `AdminPhoneNumber`) VALUES (@AdminName, @AdminPassword, @AdminPhoneNumber);";
                
                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.CommandText = query;

                mysqlCommand.Parameters.AddWithValue("@AdminName", admin.AdminName);
                mysqlCommand.Parameters.AddWithValue("@AdminPassword", HashedPassword);
                mysqlCommand.Parameters.AddWithValue("@AdminPhoneNumber", admin.AdminPhoneNumber);
                
                string json = string.Empty;
                using (DatabaseManager databaseManager = new DatabaseManager()){
                    databaseManager.Insert(mysqlCommand);
                }
                return "inserted into";
            }
            return "No Access";
        }

        [HttpDelete]
        public string DeleteAdmin(AdminType admin)
        {
            if (admin.token == OldToken.token && OldToken.token != ""){
                string query = $"DELETE FROM `admin` WHERE `adminId`=@AdminId;";

                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.CommandText = query;

                mysqlCommand.Parameters.AddWithValue("@AdminId", admin.AdminId);

                using (DatabaseManager databaseManager = new DatabaseManager()){
                    databaseManager.Delete(mysqlCommand);
                }
                return "Admin Deleted";
            }
            return "No Access";
        }


        [HttpPut]
        public string PutAdmin(AdminType admin)
        {
            if (admin.token == OldToken.token && OldToken.token != ""){
                string HashedPassword = EncryptionHandler.StringHashPassword(admin.AdminPassword);

                string query = $"UPDATE `admin` SET `AdminName`=@AdminName,`AdminPassword`=@AdminPassword,`AdminPhoneNumber`=@AdminPhoneNumber WHERE `AdminId`=@AdminId;";
                
                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.CommandText = query;

                mysqlCommand.Parameters.AddWithValue("@AdminName", admin.AdminName);
                mysqlCommand.Parameters.AddWithValue("@AdminPassword", HashedPassword);
                mysqlCommand.Parameters.AddWithValue("@AdminPhoneNumber", admin.AdminPhoneNumber);
                mysqlCommand.Parameters.AddWithValue("@AdminId", admin.AdminId);

                using (DatabaseManager databaseManager = new DatabaseManager()){
                    databaseManager.Update(mysqlCommand);
                }
                return "admin Updated";
            }
            return "No Access";
        }
    }
}
