using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.Core.Extensions
{
    public static class IdentityHelper
    {
        public static async Task<IdentityResult> CreateUser(this UserManager<User> userManager, string email, string password, string role)
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

            return result;
        }

        public static async Task EnsureCreateRole(this RoleManager<IdentityRole<int>> roleManager, string role)
        {
            if (await roleManager.FindByNameAsync(role) is null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }
}