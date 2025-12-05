using AidManager.Services.Payments.Application;
using AidManager.Services.Payments.Infrastructure;
using AidManager.Services.Payments.Infrastructure.Persistence;
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
        Title = "Payments",
        Version = "v1",
        Description = "Payments Endpoints for AidManager."
    });
});

var app = builder.Build();

// 🟦 DB Migration 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
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

// Controllers
app.MapControllers();


app.Run();