using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.Core.Extensions;

namespace WebAutopark.DataAccess
{
    public static class WebAutoparkContextInitialization
    {
        public static async Task InitDbContext(WebAutoparkContext context)
        {
            await InitVehicleTypes(context);
            await InitVehicles(context);
            await InitOrders(context);
        }

        private static async Task InitOrders(WebAutoparkContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                var orders = new []
                {
                    new Order { 
                        UserId = 1,
                        Products = new Product[]
                        {
                           new Vehicle { Id = 2, ProductAmount = 1, Price = 200000 },
                           new Vehicle { Id = 4, ProductAmount = 1, Price = 2000000 }
                        }
                    },
                };

                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitVehicles(WebAutoparkContext context)
        {
            if (!await context.Vehicles.AnyAsync())
            {
                var vehicles = new []
                {
                    new Vehicle{ ModelName = "Opel", Price = 20_000m, ProductAmount = 30 },
                };

                await context.Vehicles.AddRangeAsync(vehicles);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitVehicleTypes(WebAutoparkContext context)
        {
            if (!await context.VehicleTypes.AnyAsync())
            {
                var vehicleTypes = new []
                {
                    new VehicleType()
                };

                await context.VehicleTypes.AddRangeAsync(vehicleTypes);
                await context.SaveChangesAsync();
            }
        }
        
        public static async Task InitUsers(UserManager<User> userManager, IConfiguration configuration)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var adminSection = configuration.GetSection("AdminAccount");
                var adminEmail = adminSection["Login"];
                var password = adminSection["Password"];

                await userManager.CreateUser(adminEmail, password, IdentityRoleConstant.Admin);

                await userManager.CreateUser("user1@gmail.com", "12345678", IdentityRoleConstant.User);
                await userManager.CreateUser("user2@gmail.com", "12345678", IdentityRoleConstant.User);
            }
        }
        
        public static async Task InitRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.EnsureCreateRole(IdentityRoleConstant.Admin);
                
                await roleManager.EnsureCreateRole(IdentityRoleConstant.User);
            }
        }
    }
}