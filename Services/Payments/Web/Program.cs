using AidManager.Services.Payments.Application;
using AidManager.Services.Payments.Infrastructure;
using AidManager.Services.Payments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Consul;

var builder = WebApplication.CreateBuilder(args);

// 🟦 Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Payments",
        Version = "v1",
        Description = "Payments Endpoints for AidManager."
    });
});

var app = builder.Build();

// 🟦 DB Migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

// Healthcheck
app.MapGet("/health", () => Results.Ok("Healthy"));

// Controllers
app.MapControllers();

// 🟦 Consul Configuration
var port = int.Parse(builder.Configuration["ServicePort"] ?? "80");
var serviceHost = builder.Configuration["ServiceHost"] ?? "payments-service";

var consul = new ConsulClient(config =>
{
    config.Address = new Uri("http://consul:8500");
});

var registration = new AgentServiceRegistration()
{
    ID = $"payments-service-{Guid.NewGuid()}",
    Name = "payments-service",
    Address = serviceHost,
    Port = port,
    Check = new AgentServiceCheck()
    {
        HTTP = $"http://{serviceHost}/health",
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
    }
};

await consul.Agent.ServiceRegister(registration);

app.Run();
