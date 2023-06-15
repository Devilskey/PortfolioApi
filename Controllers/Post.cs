using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers;

[ApiController]
[Route("")]
public class Post : ControllerBase
{
    [Route("PostManager")]
    [HttpGet]
    public string GetPostCommand()
    {
        string query = $"SELECT * FROM post;";
        string json = string.Empty;
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;
        using (DatabaseHandler databaseManager = new DatabaseHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }
        return json;
    }

    [Route("PostManager")]
    [HttpPost]
    public ActionResult<Types.Post> PostPostCommand(Types.Post postData)
    {
        if (Token.isExpired())
            return BadRequest();

        if (postData.Token != Token.token || Token.token == "")
            return BadRequest();

        string query = $"INSERT INTO post( `PostTitle`, `PostContent`, `PostTag`, `thumbnail`) VALUES (@PostTitle, @PostContent, @PostTag, @PostThumbnail);";
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostTitle", postData.PostTitle);
        mysqlCommand.Parameters.AddWithValue("@PostContent", postData.PostContent);
        mysqlCommand.Parameters.AddWithValue("@PostTag", postData.PostTag);
        mysqlCommand.Parameters.AddWithValue("@PostThumbnail", postData.thumbnail);
            
        using (DatabaseHandler databaseManager = new DatabaseHandler())
            databaseManager.EditDatabase(mysqlCommand);
        return Ok();
    }

    [Route("PostManager")]
    [HttpDelete]
    public string DeletePostCommand(Types.Post postData)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (postData.Token != Token.token || Token.token == "")
            return $"No token No access";

        string query = $"DELETE FROM post WHERE postId=@PostId";

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);

        using (DatabaseHandler databaseManager = new DatabaseHandler())
            databaseManager.EditDatabase(mysqlCommand);
        return "Deleted";
        

    }

    [Route("PostManager")]
    [HttpPut]
    public string EditPostCommand(Types.Post postData)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (postData.Token != Token.token || Token.token == "")
            return $"No token No access";

        string query = $"UPDATE post SET `PostTitle`=@PostTitle, `PostContent`=@PostContent, `thumbnail`=@Thumbnail WHERE `postId`=@PostId;";
            
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostTitle", postData.PostTitle);
        mysqlCommand.Parameters.AddWithValue("@PostContent", postData.PostContent);
        mysqlCommand.Parameters.AddWithValue("@Thumbnail", postData.thumbnail);
        mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);
           
        using (DatabaseHandler databaseManager = new DatabaseHandler())
            databaseManager.EditDatabase(mysqlCommand);
        return $"Updated {postData.postId}";
    }

    [Route("PostMenu")]
    [HttpGet]
    public string getCommand()
    {
        string query = $"SELECT postId, PostTitle FROM post;";
        string json = string.Empty;

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        using (DatabaseHandler databaseManager = new DatabaseHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }

        return json;
    }

    [Route("PostMenu")]
    [HttpPost]
    public string postCommand(Types.Post postData)
    {
        string query = $"SELECT PostTitle, PostContent, thumbnail FROM post WHERE postId=@PostId;";
        string json = string.Empty;

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);

        using (DatabaseHandler databaseManager = new DatabaseHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }

        return json;
    }

    [Route("PostMenuSite")]
    [HttpGet]
    public string GetCommandSiteMenu()
    {
        string query = $"SELECT postId, PostTitle, thumbnail FROM post;";
        string json = string.Empty;

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        using (DatabaseHandler databaseManager = new DatabaseHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }

        return json;
    }
}
