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

// 🟦 DB INITIALIZATION 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var db = services.GetRequiredService<ProfilesDbContext>();
        
        
        db.Database.EnsureCreated(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

// Healthcheck
app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapControllers();
app.Run();