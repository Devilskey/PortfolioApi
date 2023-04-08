using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Seeder;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Seeder : ControllerBase
    {
        [HttpGet]
        public string Seed()
        {
            return SeederDataBase.Seeder();
        }
    }
}
