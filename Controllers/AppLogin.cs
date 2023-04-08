using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppLogin : ControllerBase
    {
        
        public static AdminData LoginData = new AdminData();
        public static int GiveId = 1;
        private static IMemoryCache cache;

        public AppLogin() { }

        [HttpPost]
        public string PostCommand(AdminLogin adminLoginData)
        {
            string query = $"SELECT AdminName, AdminPassword FROM adminlogin WHERE AdminName = '{adminLoginData.Username}';";
            string json = string.Empty;
            using (DatabaseManager databaseManger = new DatabaseManager())
            {
                json = databaseManger.Select(query);
                json = json.Replace("[", "");
                json = json.Replace("]", "");
            }

            try
            {
               LoginData = JsonConvert.DeserializeObject<AdminData>(json);
            }catch(Exception ex) { return "No Access"; }

            if (LoginData == null) return "User not found";

            if (LoginData.AdminName != adminLoginData.Username || LoginData.AdminPassword != adminLoginData.Password)
                return json;

            TokenManager.TokenGenerator();

            return TokenType.token;
        }

    }
}
