using AidManager.Services.Payments.Application.Interfaces;
using AidManager.Services.Payments.Infrastructure.Persistence;
using AidManager.Services.Payments.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AidManager.Services.Payments.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    MySqlServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            // Register Repositories
            services.AddScoped<IPaymentDetailRepository, PaymentDetailRepository>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


            return services;
        }
    }
}
