using AidManager.API.Services.Profiles.Application;
using AidManager.API.Services.Profiles.Infrastructure;
using AidManager.API.Services.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Consul;

var builder = WebApplication.CreateBuilder(args);

// 🟦 Services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Profiles",
        Version = "v1",
        Description = "Profiles Endpoints for AidManager."
    });
});

var app = builder.Build();

// 🟦 DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProfilesDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

// Healthcheck
app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();

// 🟦 Consul Config
var port = int.Parse(builder.Configuration["ServicePort"] ?? "80");
var serviceHost = builder.Configuration["ServiceHost"] ?? "profiles-service";

var consulClient = new ConsulClient(config =>
{
    config.Address = new Uri("http://consul:8500");
});

var registration = new AgentServiceRegistration()
{
    ID = $"profiles-service-{Guid.NewGuid()}",
    Name = "profiles-service",
    Address = serviceHost,
    Port = port,
    Check = new AgentServiceCheck()
    {
        HTTP = $"http://{serviceHost}/health",
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
    }
};

await consulClient.Agent.ServiceRegister(registration);

app.Run();
