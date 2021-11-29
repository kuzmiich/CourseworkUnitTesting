using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Extensions
{
    public static class MiddlewareRepositoryExtensions
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Detail>, DetailRepository>();

            /*services.AddScoped<IRepository<Vehicle>, VehicleRepository>();

            services.AddScoped<IRepository<VehicleType>, VehicleTypeRepository>();

            services.AddScoped<IRepository<OrderDetail>, OrderDetailRepository>();

            services.AddScoped<IRepository<OrderVehicle>, OrderVehicleRepository>();*/
            
            return services;
        }
        
        public static IServiceCollection AddIdentityContext(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
                {
                    options.Password = new PasswordOptions
                    {
                        RequiredLength = 6,
                        RequireNonAlphanumeric = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                        RequireDigit = false,
                    };
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<WebAutoparkContext>()
                .AddDefaultTokenProviders();

            return services;
        }
        
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebAutoparkContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DevelopmentDB"));
            });
            return services;
        }
    }
}