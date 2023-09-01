using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace webApi.Managers;

public class DatabaseMysqlHandler : IDisposable
{
    private string username { get; set; }
    private string password { get; set; }
    private string server { get; set; }
    private string databaseName { get; set; }
    private string connectionString { get; set; }
    private MySqlConnection connection { get; set; }

    private static string Worked = "";
    private bool NoConnectionPossible = false;

    public void Dispose()
    {
        username = "";
        password = "";
        server = "";
        databaseName = "";
        connection.Close();
    }



    public DatabaseMysqlHandler()
    {
        Initialize();
    }

    void Initialize()
    {
        username = Environment.GetEnvironmentVariable("UsernameDb") ?? "root";
        password = Environment.GetEnvironmentVariable("PasswordDb") ?? "password";
        server = Environment.GetEnvironmentVariable("ServerDb") ?? "10.0.0.12";
        databaseName = Environment.GetEnvironmentVariable("DatabaseName") ?? "SmortTestDb";

        Console.WriteLine($"server={server};port=3306;uid={username};pwd={password};database={databaseName};");
        connectionString = $"server={server};port=3306;uid={username};pwd={password};database={databaseName};";
        connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
        } catch (Exception ex)
        {
            NoConnectionPossible = true;
        }
        Worked = connection.State.ToString();
    }

    public bool PingDatabase()
    {
        if (NoConnectionPossible)
            return NoConnectionPossible;

        return connection.Ping();
    }

    public string Select(MySqlCommand SqlCommand)
    {
        string json = string.Empty;
        SqlCommand.Connection = connection;
        try
        {
            using (MySqlDataReader Reader = SqlCommand.ExecuteReader())
            {
                json = sqlReaderToJson(Reader);
            }
        }
        catch (Exception ex)
        {
            json = $"Json Error {ex} : {Worked}";
        }
        return json;
    }

    public void EditDatabase(MySqlCommand SqlCommand)
    {
        SqlCommand.Connection = connection;
        try
        {
            SqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public string sqlReaderToJson(MySqlDataReader reader)
    {
        List<object> objects = new List<object>();
        while (reader.Read())
        {
            IDictionary<string, object> record = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                record.Add(reader.GetName(i), reader[i]);
            }
            objects.Add(record);
        }
        string json = JsonConvert.SerializeObject(objects);
        return json;
    }

    public void SendImport(string[] sqlFileContent)
    {
        MySqlCommand sqlCommand = new MySqlCommand();
        foreach (string query in sqlFileContent)
        {
            sqlCommand.CommandText = query;
            sqlCommand.Connection = connection;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}