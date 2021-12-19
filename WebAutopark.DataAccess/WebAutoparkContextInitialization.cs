using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.Core.Extensions;

namespace WebAutopark.DataAccess
{
    public static class WebAutoparkContextInitialization
    {
        public static async Task InitDbContext(WebAutoparkContext context)
        {
            await InitVehicleTypes(context);
            await InitOrders(context);
            await InitProducts(context);
        }

        private static async Task InitProducts(WebAutoparkContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                var products = new []
                {
                    new Product
                    {
                        Name = "Lamba", 
                        Price = 1000000m,
                        ImgUrl = "https://topworldauto.com/photos/Lamborghini/ce/0f/" +
                                 "6282_lamborghini-murcielago-sv-670-4-flickr-photo-sharing.jpg",
                        Description = "Some text"
                    },
                    new Product
                    {
                        Name = "Mustang", 
                        Price = 50000m,
                        ImgUrl = "https://image.winudf.com/v2/image/" +
                                 "Y29tLkhvbWVMYW5kU3R1ZGlvcy5DYXJzLkZhc3QuRmFzdENhci5TcG9ydHNDYXIuUmVkLk11c3RhbmdDYXJzLlJlZE11c3RhbmdDYXJzV2FsbHBhcGVyX3NjcmVlbl8xMF8xNTEyMjIyOTI5XzAyMg/screen-10.jpg?fakeurl=1&type=.jpg",
                        Description = "Some text 2"
                    },
                    new Product
                    {
                        Name = "Maserati", 
                        Price = 30000m,
                        ImgUrl = "https://i.pinimg.com/originals/a4/8b/b8/a48bb8dc1128710ec5b711ed1bfc038e.jpg",
                        Description = "Some text 3"
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitOrders(WebAutoparkContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                var orders = new Order[]
                {
                };

                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitVehicleTypes(WebAutoparkContext context)
        {
            if (!await context.VehicleTypes.AnyAsync())
            {
                var vehicleTypes = new[]
                {
                    new VehicleType() { TaxCoefficient = 2, TypeName = "Type1" },
                    new VehicleType() { TaxCoefficient = 1, TypeName = "Type2" }
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