using Application;
using Consul;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🟦 Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Tasks",
        Version = "v1",
        Description = "Task Endpoints for ManageWise."
    });
});

var app = builder.Build();

// 🟦 DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TasksDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapControllers();

// 🟦 Consul
var port = int.Parse(builder.Configuration["ServicePort"] ?? "80");
var serviceHost = builder.Configuration["ServiceHost"] ?? "tasks-service";

var consulClient = new ConsulClient(config =>
{
    config.Address = new Uri("http://consul:8500");
});

var registration = new AgentServiceRegistration()
{
    ID = $"tasks-service-{Guid.NewGuid()}",
    Name = "tasks-service",
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
