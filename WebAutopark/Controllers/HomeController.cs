using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var products = _mapper.Map<List<ProductViewModel>>(await _productService.GetAll());
            
            return View(products ?? new List<ProductViewModel>());
        }

        /// <summary>
        ///     You can look examples of logger events if you will run the action.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/CheckLogger")]
        public IActionResult CheckLogger()
        {
            _logger.LogTrace("Trace log message");
            _logger.LogDebug("Debug log message");
            _logger.LogInformation("Info log message");
            _logger.LogWarning("Warn log message");
            _logger.LogError("Error log message");
            _logger.LogCritical("Fatal log message");

            return RedirectToAction("Index");
        }
        

        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}