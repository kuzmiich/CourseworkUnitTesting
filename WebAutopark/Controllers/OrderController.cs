using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Constants;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, ICartService cartService,
            UserManager<User> userManager)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpGet("index/")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            const int adminId = 1;
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var userId = int.Parse(await _userManager.GetUserIdAsync(user));

            var orders = await _orderService.GetAll();
            var mappedOrders = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            if (userId == adminId)
            {
                return View(mappedOrders);
            }

            var userOrders = mappedOrders.Where(o => o.UserId == userId);

            return View(userOrders);
        }

        [HttpGet("create/")]
        [Authorize]
        public async Task<IActionResult> OrderCreate(int userId)
        {
            var cartItemModels = await _cartService.GetAll();
            var cartItemViewModels = _mapper.Map<List<ShoppingCartItemViewModel>>(cartItemModels);

            var orderViewModel = new OrderViewModel
            {
                CartItems = cartItemViewModels,
                UserId = userId,
                TotalPrice = _cartService.GetTotalPrice(cartItemModels)
            };

            return View(orderViewModel);
        }

        [HttpPost("create/")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderCreate(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderModel = _mapper.Map<OrderModel>(orderViewModel);
                await _cartService.SetCartItemsAndTotalPrice(orderModel);
                
                await _orderService.Create(orderModel);

                return RedirectToAction("Index");
            }

            var model = new OrderViewModel
            {
                CartItems = _mapper.Map<List<ShoppingCartItemViewModel>>(await _cartService.GetAll())
            };

            return View(model);
        }

        [HttpGet("update/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> OrderUpdate(int id)
        {
            var updateModel = await _orderService.GetById(id);

            if (updateModel is null)
                return NotFound();

            return View(_mapper.Map<OrderViewModel>(updateModel));
        }
        
        [HttpPost("update/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderUpdate(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Update(_mapper.Map<OrderModel>(orderViewModel));

                return RedirectToAction("Index");
            }

            return View(orderViewModel);
        }

        [HttpGet("delete/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ActionName("OrderDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _orderService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<OrderViewModel>(deleteModel));
        }

        [HttpPost("delete/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> OrderDelete(int id)
        {
            await _orderService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}