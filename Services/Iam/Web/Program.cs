using Application;
using Microsoft.EntityFrameworkCore;
using Consul;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "IAM",
        Version = "v1",
        Description = "IAM Endpoints for ManageWise."
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IamDbContext>();
    db.Database.Migrate(); // Applies any pending migrations
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

var port = 5289;
var useHttps = false;

app.MapGet("/health", () => Results.Ok("Healthy"));

var consulClient = new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500");
});

var registration = new AgentServiceRegistration()
{
    ID = $"iam-service-{Guid.NewGuid()}",
    Name = "iam-service",
    Address = "127.0.0.1",
    Port = port,
    Check = new AgentServiceCheck()
    {
        HTTP = $"{(useHttps ? "https" : "http")}://127.0.0.1:{port}/health",
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1), // opcional
        TLSSkipVerify = useHttps
    }
};

await consulClient.Agent.ServiceRegister(registration);

app.Run();

