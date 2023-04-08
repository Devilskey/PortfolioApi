using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Seeder;

namespace webApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Seeder : ControllerBase
    {
        [HttpGet]
        public string Seed()
        {
            return SeederDataBase.Seeder();
        }
    }
}
