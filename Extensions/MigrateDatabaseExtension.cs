using Serilog;

namespace webApi.Extensions
{
    public static class MigrateDatabaseExtension
    {
        public static IServiceCollection MigrateDatabase(this IServiceCollection service, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            service.AddLogging(options => options.AddSerilog());
            return service;
        }
    }
}
