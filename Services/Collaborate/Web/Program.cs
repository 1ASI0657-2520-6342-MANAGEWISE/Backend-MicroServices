using AidManager.Collaborate.Application; // For AddApplicationServices
using AidManager.Collaborate.Infrastructure; // For AddInfrastructureServices

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register services from Application and Infrastructure layers
builder.Services.AddApplicationServices(); // From AidManager.Collaborate.Application
builder.Services.AddInfrastructureServices(builder.Configuration); // From AidManager.Collaborate.Infrastructure

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection(); // Typically enabled for production

app.UseAuthorization();

app.MapControllers();

app.Run();
