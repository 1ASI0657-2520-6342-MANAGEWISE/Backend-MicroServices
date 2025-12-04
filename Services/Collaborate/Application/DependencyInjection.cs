using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection; // Required for Assembly.GetExecutingAssembly() if you choose that method

namespace AidManager.Collaborate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registers MediatR services and handlers from the assembly containing this DependencyInjection class.
        // MediatR v11.x.x changed how this is done.
        // For MediatR 11.1.0, the registration is typically done by specifying an assembly.
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        // Or, if you prefer, you can use Assembly.GetExecutingAssembly()
        // services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
