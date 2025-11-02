using Application;
using Consul;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task",
        Version = "v1",
        Description = "Task Endpoints for ManageWise."
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TasksDbContext>();
    db.Database.Migrate(); 
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok("Healthy"));

// 🔹 Autorizar y mapear controladores
app.UseAuthorization();
app.MapControllers();

// 🔹 Configuración de Consul
var port = 5282; 
var useHttps = false;

var consulClient = new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500");
});

// 🔹 Registro del servicio en Consul
var registration = new AgentServiceRegistration()
{
    ID = $"task-service-{Guid.NewGuid()}",
    Name = "task-service",
    Address = "127.0.0.1",
    Port = port,
    Check = new AgentServiceCheck()
    {
        HTTP = $"{(useHttps ? "https" : "http")}://127.0.0.1:{port}/health",
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
        TLSSkipVerify = useHttps
    }
};

await consulClient.Agent.ServiceRegister(registration);

app.Run();
