using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;

        public UserController(ILogger<AccountController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            
            return View(users);
        }
        
        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await _userManager.Users.SingleAsync(u => u.Id == id);
            
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}