using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Infrastructure.Persistence;
using AidManager.API.Services.Profiles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System; 

namespace AidManager.API.Services.Profiles.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 32)); 

        services.AddDbContext<ProfilesDbContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion,
                mySqlOptions =>
                {
                    
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10, 
                        maxRetryDelay: TimeSpan.FromSeconds(30), 
                        errorNumbersToAdd: null);
                });
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDeletedUserRepository, DeletedUserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}