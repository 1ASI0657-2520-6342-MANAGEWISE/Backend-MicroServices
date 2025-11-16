using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Cargar configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 🔹 Registrar Ocelot con Consul
builder.Services.AddOcelot().AddConsul();

var app = builder.Build();

// 🔹 Ejecutar middleware de Ocelot 
await app.UseOcelot();

app.Run();