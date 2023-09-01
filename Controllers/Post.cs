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
        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }
        return json;
    }

    [Route("CreateNewPost")]
    [HttpPost]
    public ActionResult<PostObject> CreateNewPost(PostObject postData)
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
            
        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
            databaseManager.EditDatabase(mysqlCommand);
        return Ok();
    }

    [Route("DeletePost")]
    [HttpDelete]
    public string DeletePostCommand(PostObject postData)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (postData.Token != Token.token || Token.token == "")
            return $"No token No access";

        string query = $"DELETE FROM post WHERE postId=@PostId";

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);

        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
            databaseManager.EditDatabase(mysqlCommand);
        return "Deleted";
    }

    [Route("EditPost")]
    [HttpPut]
    public string EditPost(PostObject postData)
    {
        if (Token.isExpired())
            return "Token Expired";

        if (postData.Token != Token.token || Token.token == "")
            return $"No token No access";

        string query = $"UPDATE post SET `PostTitle`=@PostTitle, `PostContent`=@PostContent, `thumbnail`=@Thumbnail, 'PostTag'=@PostTags WHERE `postId`=@PostId;";
            
        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostTitle", postData.PostTitle);
        mysqlCommand.Parameters.AddWithValue("@PostContent", postData.PostContent);
        mysqlCommand.Parameters.AddWithValue("@Thumbnail", postData.thumbnail);
        mysqlCommand.Parameters.AddWithValue("@PostTags", postData.PostTag);
        mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);
           
        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
            databaseManager.EditDatabase(mysqlCommand);
        return $"Updated {postData.postId}";
    }

    [Route("GetPostSimpelData")]
    [HttpGet]
    public string GetPostSimpelData()
    {
        string query = $"SELECT postId, PostTitle FROM post;";
        string json = string.Empty;

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }

        return json;
    }

    [Route("GetpostContent")]
    [HttpPost]
    public string GetpostContent(PostObject postData)
    {
        string query = $"SELECT PostTitle, PostContent, thumbnail FROM post WHERE postId=@PostId;";
        string json = string.Empty;

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        mysqlCommand.Parameters.AddWithValue("@PostId", postData.postId);

        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }

        return json;
    }

    [Route("getListPostsSite")]
    [HttpGet]
    public string getListPostsSite()
    {
        string query = $"SELECT postId, PostTitle, thumbnail, PostTag FROM post;";
        string json = string.Empty;

        MySqlCommand mysqlCommand = new MySqlCommand();
        mysqlCommand.CommandText = query;

        using (DatabaseMysqlHandler databaseManager = new DatabaseMysqlHandler())
        {
            json = databaseManager.Select(mysqlCommand);
        }

        return json;
    }
}
