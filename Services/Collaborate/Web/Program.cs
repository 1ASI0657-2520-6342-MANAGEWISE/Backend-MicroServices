using AidManager.Collaborate.Application;     
using AidManager.Collaborate.Infrastructure;  
using AidManager.Collaborate.Infrastructure.Persistence; 
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Consul;

var builder = WebApplication.CreateBuilder(args);

// 🟦 Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpClient<IProfilesClient, ProfilesClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Profiles:BaseUrl"] 
                                 ?? "http://profiles-service");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Collaborate",
        Version = "v1",
        Description = "Collaborate Endpoints for AidManager."
    });
});

var app = builder.Build();

// 🟦 DB INITIALIZATION 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<CollaborateDbContext>();
        
        Console.WriteLine("[DEBUG] Creando tablas para Collaborate si no existen...");
        db.Database.EnsureCreated();
        Console.WriteLine("[DEBUG] Base de datos Collaborate lista.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapControllers();

// 🟦 Consul 
var port = int.Parse(builder.Configuration["ServicePort"] ?? "80");
var serviceHost = builder.Configuration["ServiceHost"] ?? "collaborate-service";

var consul = new ConsulClient(config =>
{
    config.Address = new Uri("http://consul:8500");
});

var registration = new AgentServiceRegistration()
{
    ID = $"collaborate-service-{Guid.NewGuid()}",
    Name = "collaborate-service",
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