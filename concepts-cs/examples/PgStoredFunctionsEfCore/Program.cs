using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Concepts.Examples.PgStoredFunctionsEfCore.Contexts;
using Concepts.Examples.PgStoredFunctionsEfCore.DataProcessing;
using Concepts.Examples.PgStoredFunctionsEfCore.Models;

namespace Concepts.Examples.PgStoredFunctionsEfCore;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = host.Services.GetRequiredService<IExampleInstance>();
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                ConfigureServices(services);
            });

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register application instance.
        services.AddSingleton<IExampleInstance, ExampleInstance>();

        // Configure DbContext.
        services.AddDbContext<DbDataProcessingContext>((serviceProvider, options) =>
        {
            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=csconcepts;Username=postgres;Password=postgres");
        });

        // Services.
        services.AddSingleton<DbDataProcessing>();
    }
}