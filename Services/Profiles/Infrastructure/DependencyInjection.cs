using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Infrastructure.Persistence;
using AidManager.API.Services.Profiles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AidManager.API.Services.Profiles.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProfilesDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                                 MySqlServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                                 ));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeletedUserRepository, DeletedUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
