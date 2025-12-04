using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AidManager.Collaborate.Infrastructure.Persistence;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Infrastructure.Repositories;

namespace AidManager.Collaborate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure DbContext
        // The user advised to use the same connection string as in Iam/Web.
        // We will fetch this connection string in Program.cs of the Web project and pass it,
        // or expect it to be named "DefaultConnection" as is common.
        services.AddDbContext<CollaborateDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), // This name should match the one in appsettings.json
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
            ));

        // Register Repositories
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IFavoritePostRepository, FavoritePostRepository>();
        services.AddScoped<ILikedPostRepository, LikedPostRepository>();
        // Add other repositories here as they are created

        return services;
    }
}
