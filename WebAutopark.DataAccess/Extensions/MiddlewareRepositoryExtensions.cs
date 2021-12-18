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
            services.AddScoped<ICartRepository<ShoppingCartItem>, ShoppingCartRepository>();
            
            services.AddScoped<IRepository<Product>, ProductRepository>();

            services.AddScoped<IRepository<VehicleType>, VehicleTypeRepository>();

            services.AddScoped<IRepository<Order>, OrderRepository>();
            
            return services;
        }
        
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.User.RequireUniqueEmail = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                }).AddEntityFrameworkStores<WebAutoparkContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            
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