using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Services;
using WebAutopark.DataAccess.Extensions;
using AutoMapper;

namespace WebAutopark.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IDetailService, DetailService>();

            services.AddDatabaseDependencies();

            return services;
        }

        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services, params Assembly[] otherMapperAssemblies)
        {
            var assemblies = new List<Assembly>(otherMapperAssemblies)
            {
                Assembly.GetExecutingAssembly()
            };
            
            services.AddAutoMapper(assemblies);
            
            return services;
        }

        public static IServiceCollection AddCustomSolutionConfigs(this IServiceCollection services,
                                                                  IConfiguration configuration,
                                                                  params Assembly[] otherMapperAssemblies)
        {
            services.AddDependencyInjections()
                    .AddAutomapperProfiles(otherMapperAssemblies)
                    .AddDatabaseContext(configuration)
                    .AddIdentityContext();

            return services;
        }
    }
}