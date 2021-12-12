using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.DataAccess
{
    public class WebAutoparkContextInit
    {
        public static async Task InitDbContext(WebAutoparkContext context)
        {
            await InitDetails(context);
        }

        private static async Task InitDetails(WebAutoparkContext context)
        {
            if (!await context.Details.AnyAsync())
            {
                var details = new Detail[]
                {
                    new Detail { Name = "Wheel", Price = 200m, ProductAmount = 30 },
                    new Detail { Name = "oil filter", Price = 20m, ProductAmount = 10 },
                    new Detail { Name = "body side moulding", Price = 20m, ProductAmount = 20 },
                    new Detail { Name = "window frame", Price = 20m, ProductAmount = 15 },
                    new Detail { Name = "steering wheel", Price = 20m, ProductAmount = 8 },
                    new Detail { Name = "wiper", Price = 20m, ProductAmount = 60 },
                };

                await context.Details.AddRangeAsync(details);
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

                await CreateUser(adminEmail, password, IdentityRoleConstant.Admin, userManager);

                await CreateUser("user1@gmail.com", "12345678", IdentityRoleConstant.User, userManager);
                await CreateUser("user2@gmail.com", "12345678", IdentityRoleConstant.User, userManager);
            }
        }

        private static async Task CreateUser(string email, string password, string role, UserManager<User> userManager)
        {
            if (await userManager.FindByNameAsync(email) is null)
            {
                var user = new User 
                {
                    Email = email,
                    UserName = email 
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static async Task InitRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {

                if (await roleManager.FindByNameAsync(IdentityRoleConstant.Admin) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(IdentityRoleConstant.Admin));
                }

                if (await roleManager.FindByNameAsync(IdentityRoleConstant.User) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(IdentityRoleConstant.User));
                }
            }
        }
    }
}