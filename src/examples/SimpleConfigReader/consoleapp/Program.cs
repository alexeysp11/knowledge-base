using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using SimpleConfigReader.Core.ConfigurationManagers;
using SimpleConfigReader.Core.Models;
using SimpleConfigReader.Core.ObjectPooling;

namespace SimpleConfigReader.ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = host.Services.GetRequiredService<IStartupInstance>();
        app.Start();
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
        services.AddSingleton<IStartupInstance, StartupInstance>();

        // Configuration managers.
        services.AddSingleton<CsvConfigurationManager>();
        services.AddSingleton<XmlConfigurationManager>();

        // Object pooling.
        services.AddSingleton<IConfigurationPool, ConcurrentConfigurationPool>();

        // Configuration settings.
        var appsettingsConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var xmlSettings = GetXmlSettings(appsettingsConfig);
        var csvSettings = GetCsvSettings(appsettingsConfig);
        services.AddSingleton(xmlSettings);
        services.AddSingleton(csvSettings);
        services.AddSingleton(new CommonConfigSettings
        {
            DirectoryPath = appsettingsConfig.GetValue<string>("commonConfigSettings:directoryPath"),
            UseAsyncReading = appsettingsConfig.GetValue<bool>("commonConfigSettings:useAsyncReading")
        });
    }

    private static XmlSettings GetXmlSettings(IConfigurationRoot appsettingsConfig)
    {
        var xmlSettings = new XmlSettings
        {
            RootObjectName = appsettingsConfig.GetValue<string>("xml:rootElementName"),
            FieldMappings = new Dictionary<string, List<string>>()
        };
        xmlSettings.FieldMappings["Name"] = appsettingsConfig.GetSection("xml:classFields:Name").Get<List<string>>();
        xmlSettings.FieldMappings["Description"] = appsettingsConfig.GetSection("xml:classFields:Description").Get<List<string>>();
        
        return xmlSettings;
    }

    private static CsvSettings GetCsvSettings(IConfigurationRoot appsettingsConfig)
    {
        var csvSettings = new CsvSettings
        {
            ClassFieldIndexes = new Dictionary<string, int>(),
            Separators = appsettingsConfig.GetSection("csv:separators").Get<List<string>>()
        };
        csvSettings.ClassFieldIndexes["Name"] = appsettingsConfig.GetValue<int>("csv:classFieldIndexes:Name");
        csvSettings.ClassFieldIndexes["Description"] = appsettingsConfig.GetValue<int>("csv:classFieldIndexes:Description");

        return csvSettings;
    }
}