using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostMenu : ControllerBase
    {
        [HttpGet]
        public string getCommand()
        {
            string query = $"SELECT postId, PostTitle FROM post;";
            string json = string.Empty;

            MySqlCommand mysqlCommand = new MySqlCommand();
            mysqlCommand.CommandText = query;
            
            using (DatabaseManager databaseManager = new DatabaseManager())
            {
                json = databaseManager.Select(mysqlCommand);
            }

            return json;
        }

        [HttpPost]
        public string postCommand(PostType postData)
        {
            string query = $"SELECT PostTitle, PostContent, thumbnail FROM post WHERE postId=@PostId;";
            string json = string.Empty;

            MySqlCommand mysqlCommand = new MySqlCommand();
            mysqlCommand.CommandText = query;

            mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);

            using (DatabaseManager databaseManager = new DatabaseManager())
            {
                json = databaseManager.Select(mysqlCommand);
            }

            return json;
        }
    }
}