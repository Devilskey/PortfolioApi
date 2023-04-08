using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminMenu : ControllerBase
    {
       [HttpPost]
        public string postMenuAdmin(Authentication auth)
        {
            if (auth.token == OldToken.token && OldToken.token != ""){
                string query = $"SELECT AdminId, AdminName FROM admin;";
                string json = string.Empty;
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.CommandText = query;

                using (DatabaseManager databaseManager = new DatabaseManager()){
                    json = databaseManager.Select(mySqlCommand);
                }
                return json;
            }
            return "No Access";
        }
    }
}