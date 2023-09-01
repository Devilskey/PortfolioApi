using webApi.Managers;
using webApi.Seeder;

namespace webApi.Extensions
{
    public static class MigrateDatabaseExtension
    {
        private static string SqlFile = "./Migrate.sql";

        public static IServiceCollection MigrateDatabase(this IServiceCollection service, IConfiguration configuration)
        {
            using (DatabaseMysqlHandler databaseHandler = new DatabaseMysqlHandler())
            {
                string[] sqlContent = File.ReadAllLines(SqlFile);

                databaseHandler.SendImport(sqlContent);

                return service;
            }
        }
    }
}
