using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KnowledgeBase.Examples.SimpleWebService.Core.DAL;
using KnowledgeBase.Examples.SimpleWebService.Core.Models;
using KnowledgeBase.Examples.SimpleWebService.Server.Controllers;

namespace KnowledgeBase.Examples.SimpleWebService.Server;

/// <summary>
/// Initializes the application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method in the application.
    /// </summary>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// Provides functionality for configuring services.
    /// </summary>
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Calculate path.
        var rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
        var dbPath = Path.Combine(rootPath, "data", "db", "test.db");

        // App init configs.
        services.AddSingleton<AppInitConfigs>(_ =>
        {
            return new AppInitConfigs
            {
                DbConnectionString = $"Data Source={dbPath};Version=3;",
                DeleteCacheBeforeInserting = true
            };
        });
        
        // Services.
        services.AddSingleton<CachedServerValueDAL>();
    }
}
