using Microsoft.AspNetCore.Mvc;
using webApi.Handler;
using Cassandra.Data;

namespace webApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CassanderaApiTestController : ControllerBase
    {
        [HttpGet]
        public string ConnectionTest()
        {
            using (DatabaseCassandraHandler database = new()) {
                return database.Select(new CqlCommand());
            }
            return "";
        }
    }
}
