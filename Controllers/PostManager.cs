using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostManager : ControllerBase
    {

        [HttpGet]
        public string GetPostCommand()
        {
            string query = $"SELECT * FROM post;";
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
        public ActionResult<PostType> PostPostCommand(PostType postData)
        {
            if (postData.Token == OldToken.token && OldToken.token != "")
            {
                string query = $"INSERT INTO post( `PostTitle`, `PostContent`, `PostTag`, `thumbnail`) VALUES (@PostTitle, @PostContent, @PostTag, @PostThumbnail);";
                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.CommandText = query;

                mysqlCommand.Parameters.AddWithValue("@PostTitle", postData.PostTitle);
                mysqlCommand.Parameters.AddWithValue("@PostContent", postData.PostContent);
                mysqlCommand.Parameters.AddWithValue("@PostTag", postData.PostTag);
                mysqlCommand.Parameters.AddWithValue("@PostThumbnail", postData.thumbnail);
                
                using (DatabaseManager databaseManager = new DatabaseManager())
                    databaseManager.Insert(mysqlCommand);

                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public string DeletePostCommand(PostType postData)
        {
            if (postData.Token == OldToken.token && OldToken.token != "")
            {
                string query = $"DELETE FROM post WHERE postId=@PostId";

                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.CommandText = query;

                mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);

                using (DatabaseManager databaseManager = new DatabaseManager())
                    databaseManager.Delete(mysqlCommand);
                return "Deleted";
            }
            return $"No token No access";
        }

        [HttpPut]
        public string EditPostCommand(PostType postData)
        {
            if (postData.Token == OldToken.token && OldToken.token != "")
            {
                string query = $"UPDATE post SET `PostTitle`=@PostTitle, `PostContent`=@PostContent, `thumbnail`=@Thumbnail WHERE `postId`=@PostId;";
                
                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.CommandText = query;

                mysqlCommand.Parameters.AddWithValue("@PostTitle", postData.PostTitle);
                mysqlCommand.Parameters.AddWithValue("@PostContent", postData.PostContent);
                mysqlCommand.Parameters.AddWithValue("@Thumbnail", postData.thumbnail);
                mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);
               
                using (DatabaseManager databaseManager = new DatabaseManager())
                    databaseManager.Delete(mysqlCommand);
                return $"Updated {postData.postId}";
            }
            return $"No token No access";
        }
    }
}
