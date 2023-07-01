using Serilog;

namespace webApi.Extensions;

public static class AddSerilog
{
    public static IServiceCollection AddSerilogExtension(this IServiceCollection service, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Console()
            .CreateLogger();
           
        service.AddLogging(options => options.AddSerilog());
        return service;
    }
}
