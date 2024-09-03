using Microsoft.Extensions.Hosting;
using KnowledgeBase.Examples.WorkerServiceXplatform;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseSystemd()
    .ConfigureServices(services =>
    {
        services.AddSingleton(_ =>
        {
            return new AppInitConfigs
            {
                DbConnectionString = $"Server=localhost;Database=kbwokerservicexplatform;User Id=postgres;Password=postgres;"
            };
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
