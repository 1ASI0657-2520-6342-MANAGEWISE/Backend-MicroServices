using Application;
using Microsoft.EntityFrameworkCore;
using Consul;
using Infrastructure;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "IAM",
        Version = "v1",
        Description = "IAM Endpoints for ManageWise."
    });
});

var serviceHost = builder.Configuration["ServiceHost"] ?? "iam-service";
var servicePort = int.Parse(builder.Configuration["ServicePort"] ?? "80");


var app = builder.Build();

// 🔹 Migraciones
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IamDbContext>();
    db.Database.Migrate();
}

// 🔹 Swagger
app.UseSwagger();
app.UseSwaggerUI();


// 🔹 CORS: Usar el middleware
app.UseCors(MyAllowSpecificOrigins);


// 🔹 Rutas
app.MapControllers();
app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();