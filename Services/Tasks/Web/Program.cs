using Application;
using Consul;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

// 🟦 DB INITIALIZATION & MIGRATION 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var db = services.GetRequiredService<TasksDbContext>();
        
        if (db.Database.GetPendingMigrations().Any())
        {
            Console.WriteLine("[DB] Aplicando migraciones pendientes...");
            db.Database.Migrate();
        }
        else
        {
            Console.WriteLine("[DB] Asegurando que la base de datos de Tasks exista...");
            db.Database.EnsureCreated();
        }
        Console.WriteLine("[DB] Base de datos Tasks verificada correctamente.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar la base de datos de Tasks.");
        throw; 
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapControllers();


app.Run();