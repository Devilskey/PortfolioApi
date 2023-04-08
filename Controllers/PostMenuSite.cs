using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webApi.Managers;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostMenuSite : ControllerBase
    {
        [HttpGet]
        public string GetCommandSiteMenu()
        {
            string query = $"SELECT postId, PostTitle, thumbnail FROM post;";
            string json = string.Empty;

            MySqlCommand mysqlCommand = new MySqlCommand();
            mysqlCommand.CommandText = query;
            
            using (DatabaseManager databaseManager = new DatabaseManager())
            {
                json = databaseManager.Select(mysqlCommand);
            }

            return json;
        }
    }
}