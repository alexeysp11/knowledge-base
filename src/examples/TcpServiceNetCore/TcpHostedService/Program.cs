using TcpHostedService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TcpServerService>();

var host = builder.Build();
host.Run();
