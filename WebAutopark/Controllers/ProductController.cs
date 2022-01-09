using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Constants;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("index/")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var models = await _productService.GetAll();

            return View(_mapper.Map<List<ProductViewModel>>(models));
        }

        [HttpGet("info/")]
        [Authorize]
        public async Task<IActionResult> ProductInfo(int id)
        {
            var model = await _productService.GetById(id);

            return View(_mapper.Map<ProductViewModel>(model));
        }
        
        [HttpGet("create/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public IActionResult ProductCreate()
        {
            return View();
        }
        
        [HttpPost("create/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(_mapper.Map<ProductModel>(productViewModel));
                return RedirectToAction("Index");
            }

            return View();
        }
        
        [HttpGet("update/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var updatedModel = await _productService.GetById(id);

            if (updatedModel is null)
                return NotFound();

            return View(_mapper.Map<ProductViewModel>(updatedModel));
        }

        [HttpPost("update/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductUpdate(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(_mapper.Map<ProductModel>(productViewModel));
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }

        [HttpGet("delete/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ActionName("ProductDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _productService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<ProductViewModel>(deleteModel));
        }

        [HttpPost("delete/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> ProductDelete(int id)
        {
            await _productService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}