using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Services;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.DataAccess;
using WebAutopark.DataAccess.Extensions;

namespace WebAutopark.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        private static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddDatabaseDependencies();

            return services;
        }

        public static IServiceCollection AddCustomSolutionConfigs(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDependencyInjections()
                .AddDatabaseContext(configuration)
                .AddIdentityContext();

            return services;
        }

        public static async Task ContextSeedData(this IHost application)
        {
            await using var scope = application.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<WebAutoparkContext>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var configuration = services.GetRequiredService<IConfiguration>();

                await WebAutoparkContextInitialization.InitDbContext(context);
                await WebAutoparkContextInitialization.InitRoles(roleManager);
                await WebAutoparkContextInitialization.InitUsers(userManager, configuration);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }
        }
    }
}