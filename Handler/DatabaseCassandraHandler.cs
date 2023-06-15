using Cassandra;
using Cassandra.Data;
using Cassandra.DataStax.Graph;
using MySqlX.XDevAPI;
using ISession = Cassandra.ISession;

namespace webApi.Handler
{
    public class DatabaseCassandraHandler : IDisposable
    {

        private static ICluster cluster { get; set; }
        private static ISession Session { get; set; }

        private static string Host = "localhost";
        private static string Username = "root";
        private static string Password = "password";

        public DatabaseCassandraHandler()
        {
            cluster = Cluster.Builder()
                .AddContactPoint(Host)
                .WithGraphOptions(new GraphOptions().SetName("demo"))
                .WithCredentials(Username, Password)
                .Build();
            Session = cluster.Connect();
        }
        
        
        public string Select(CqlCommand command )
        {
            var query = new SimpleStatement("SELECT * FROM system.local LIMIT 1");
            var resultSet = Session.Execute(query);
            return resultSet.ToString();
        }


        public void Dispose()
        {
            cluster.Dispose();
        }
    }
}
