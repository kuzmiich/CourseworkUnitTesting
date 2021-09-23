using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}