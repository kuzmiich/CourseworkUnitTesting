using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            
            return View(users);
        }
        

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ActionName("UserDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deletedModel = await _userManager.Users.SingleAsync(u => u.Id == id);

            if (deletedModel is null)
                return NotFound();

            return View(deletedModel);
        }
        
        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> UserDelete(int id)
        {
            var user = await _userManager.Users.SingleAsync(u => u.Id == id);
            
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}