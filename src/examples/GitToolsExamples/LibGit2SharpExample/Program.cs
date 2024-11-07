using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample;
using WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.Models;

IHost host = Host.CreateDefaultBuilder().ConfigureServices(
    services =>
    {
        services.AddSingleton<IStartupInstance, StartupInstance>();

        var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build();
        var appsettings = configuration.GetSection("LibGit2SharpExampleSettings").Get<LibGit2SharpExampleSettings>();
        services.AddSingleton(appsettings);
    }).Build();

var app = host.Services.GetRequiredService<IStartupInstance>();
app.Run();
