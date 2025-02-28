using PixelTerminalUI.ServiceEngine.Dto;
using PixelTerminalUI.ServiceEngine.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/pixelterminalui/go", (SessionInfoDto? sessionInfo) =>
{
    return sessionInfo ?? new SessionInfoDto();
});

app.Run();
