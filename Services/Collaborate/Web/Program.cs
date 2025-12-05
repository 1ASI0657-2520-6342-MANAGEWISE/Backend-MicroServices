using AidManager.Collaborate.Application;     
using AidManager.Collaborate.Infrastructure;  
using AidManager.Collaborate.Infrastructure.Persistence; 
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🟦 Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpClient<IProfilesClient, ProfilesClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["Services:Profiles:BaseUrl"] 
        ?? "http://localhost:5001" // fallback local
    );
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

app.Run();