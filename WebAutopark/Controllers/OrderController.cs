using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, ICartService cartService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAll();

            return View(_mapper.Map<IEnumerable<OrderViewModel>>(orders));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OrderInfo(int id)
        {
            var order = await _orderService.GetById(id);

            return View(_mapper.Map<OrderViewModel>(order));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OrderCreate()
        {
            var cartItemModels = await _cartService.GetAll();
            var cartItemViewModels = _mapper.Map<List<ShoppingCartItemViewModel>>(cartItemModels);
            
            var orderViewModel = new OrderViewModel()
            {
                CartItems = cartItemViewModels
            };
            return View(orderViewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OrderCreate(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Create(_mapper.Map<OrderModel>(order));
                return RedirectToAction("Index");
            }

            var orderViewModel = new OrderViewModel
            {
                CartItems = _mapper.Map<List<ShoppingCartItemViewModel>>(_cartService.GetAll())
            };

            return View(orderViewModel);
        }
        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> OrderUpdate(int id)
        {
            var updateModel = await _orderService.GetById(id);

            if (updateModel is null)
                return NotFound();

            return View(_mapper.Map<OrderViewModel>(updateModel));
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderUpdate(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Update(_mapper.Map<OrderModel>(order));
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ActionName("OrderDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _orderService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<OrderViewModel>(deleteModel));
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> OrderDelete(int id)
        {
            await _orderService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}