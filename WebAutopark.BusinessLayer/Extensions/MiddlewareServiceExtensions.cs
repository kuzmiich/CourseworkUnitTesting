using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.DataAccess;
using WebAutopark.DataAccess.Extensions;
using WebAutopark.DataAccess.Repositories;

namespace WebAutopark.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        private static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<ICartService, ShoppingShoppingCartService>();
            
            services.AddScoped<IProductService, ProductService>();

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
                .AddIdentityConfiguration();

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
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext
                .Session;
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            var context = services.GetService<WebAutoparkContext>();

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

    }
}