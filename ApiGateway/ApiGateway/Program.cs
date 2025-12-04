using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:3000", "http://localhost:4200") 
                .AllowAnyHeader() 
                .AllowAnyMethod() 
                .AllowCredentials(); 
        });
});


builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot().AddConsul();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

await app.UseOcelot();

app.Run();