using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostMenu : ControllerBase
    {

        [HttpGet]
        public string GetCommand()
        {
            string query = $"SELECT * FROM post;";
            string json = string.Empty;
            using (DatabaseManager databaseManager = new DatabaseManager())
            {
                json = databaseManager.Select(query);
            }
               // if(json == "") return "nope";

            return json;
        }
        [HttpPost]
        public List<PostType> PostCommand(PostType postData)
        {
            string query = $"SELECT * FROM post WHERE postId = {postData.PostId};";
            string json = string.Empty;
            using (DatabaseManager databaseManager = new DatabaseManager())
            {
                json = databaseManager.Select(query);
            }
            if (json == "") 
                return new List<PostType>();

            List<PostType> post = JsonConvert.DeserializeObject<List<PostType>>(json);

            return post;
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class PostManager : ControllerBase
    {

        [HttpGet]
        public string GetCommand()
        {
            string query = $"SELECT * FROM post;";
            string json = string.Empty;
            using (DatabaseManager databaseManager = new DatabaseManager())
            {
                json = databaseManager.Select(query);
            }
        

            return json;
        }

        [HttpPost]
        public ActionResult<PostType> PostCommand(PostType postData)
        {
            if (postData.Token == TokenType.token && TokenType.token != "")
            {
                string query = $"INSERT INTO post( `PostTitle`, `PostContent`, `PostTag`, `thumbnail`) VALUES ('{postData.PostTitle}','{postData.PostContent}','{postData.PostTag}', '{postData.thumbnail}');";
                using (DatabaseManager databaseManager = new DatabaseManager())
                    databaseManager.Insert(query);

                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public string DeletCommand(PostType postData)
        {
            if (postData.Token == TokenType.token && TokenType.token != "")
            {
                string query = $"DELETE FROM post WHERE postId = {postData.PostId}";
                using (DatabaseManager databaseManager = new DatabaseManager())
                    databaseManager.Delete(query);
                return "Deleted";
            }
            return $"No token No access";
        }
    }
}
