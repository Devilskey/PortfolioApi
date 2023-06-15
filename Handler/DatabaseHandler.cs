using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace webApi.Managers;

public class DatabaseHandler : IDisposable
{
    private string username { get; set; }
    private string password { get; set; }
    private string server { get; set; }
    private string databaseName { get; set; }
    private string connectionString { get; set; }
    private MySqlConnection connection { get; set; }

    private static string Worked = "";

    public void Dispose()
    {
        username = "";
        password = "";
        server = "";
        databaseName = "";
        connection.Close();
    }

    public DatabaseHandler()
    {
        Initialize();
    }

    void Initialize()
    {
        username = Environment.GetEnvironmentVariable("UsernameDb");
        password = Environment.GetEnvironmentVariable("PasswordDb");
        server = Environment.GetEnvironmentVariable("ServerDb");
        databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        if(username == "" || password == "" || server == "" || databaseName == "")
        {
            username = "root";
            password = "password";
            server = "127.0.0.1";
            databaseName = "";
            System.Console.WriteLine("No Env found");
        }

        
        System.Console.WriteLine($"server={server};port=3306;uid={username};pwd={password};database={databaseName};");
        connectionString = $"server={server};port=3306;uid={username};pwd={password};database={databaseName};";
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Worked = connection.State.ToString();
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

    public void SendImport(string query)
    {
        MySqlCommand SqlCommand = new MySqlCommand();
        SqlCommand.CommandText = query;
        SqlCommand.Connection = connection;
        try
        {
            SqlCommand.ExecuteNonQuery();
        }
        catch (Exception)
        { }
    }
}