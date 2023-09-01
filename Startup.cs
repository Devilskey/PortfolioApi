using Serilog;
using webApi.Extensions;

namespace webApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("anyCors", Policy =>
                Policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.MigrateDatabase(Configuration);
        services.AddSerilogExtension(Configuration);

        Console.WriteLine("Configuration Services Done");
    }

    public void Configure(IApplicationBuilder app)
    {

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("anyCors");

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

