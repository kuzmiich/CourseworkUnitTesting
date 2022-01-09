using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    [Route("carts")]
    public class ShoppingCartController : Controller
    {
        private readonly ICartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ShoppingCartController(ICartService shoppingCartService, IProductService productService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("index/")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var models = await _shoppingCartService.GetAll();
            
            var mappedModels = _mapper.Map<List<ShoppingCartItemViewModel>>(models);
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCarts = mappedModels
            };

            return View(shoppingCartViewModel);
        }

        [Route("add/")]
        [Authorize]
        public async Task<IActionResult> AddProduct(int id)
        {
            var product = await _productService.GetAll();
            var selectedProduct = product.FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                await _shoppingCartService.AddProduct(selectedProduct, 1);
            }
            
            return RedirectToAction("Index");
        }
        
        [Route("remove/")]
        [Authorize]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _productService.GetAll();
            var selectedProduct = product.FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                await _shoppingCartService.RemoveProduct(selectedProduct);
            }
            
            return RedirectToAction("Index");
        }
        
        [Route("clear/")]
        [Authorize]
        public async Task<IActionResult> ClearCart()
        {
            await _shoppingCartService.ClearCart();

            return RedirectToAction("Index");
        }
    }
}