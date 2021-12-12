using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.BusinessLayer.MappingProfiles;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services;
using WebAutopark.DataAccess.Extensions;

namespace WebAutopark.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        private static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IDetailService, DetailService>();
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
    }
}