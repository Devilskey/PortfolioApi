
namespace webApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IHost run = CreateHostBuilder(args)
            .Build();
        await run.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                config
                     .AddJsonFile("appsettings.json", false)
                     .AddJsonFile($"appsettings.{environmentName}.json", true)
                     .AddJsonFile("serilog.json", false)
                     .AddJsonFile($"serilog.{environmentName}.json", true)
                     .AddEnvironmentVariables();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            webBuilder
                .UseStartup<Startup>());
    }

}