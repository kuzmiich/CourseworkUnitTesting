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
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, ICartService cartService,
            UserManager<User> userManager, IRepository<Order> orderRepository)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cartService = cartService;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderCreate(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                await SetOrderCartItemsAndTotalPrice(orderViewModel);
                await _orderService.Create(_mapper.Map<OrderModel>(orderViewModel));

                return RedirectToAction("Index");
            }

            var model = new OrderViewModel
            {
                CartItems = _mapper.Map<List<ShoppingCartItemViewModel>>(await _cartService.GetAll())
            };

            return View(model);
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
        
        /*var cartItemViewModels =
            _mapper.Map<List<ShoppingCartItemViewModel>>(await _cartService.GetAll(orderViewModel.Id));
        orderViewModel.CartItems = cartItemViewModels;
        await _orderService.Update(_mapper.Map<OrderModel>(orderViewModel));*/

        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderUpdate(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var order = _orderRepository.GetAll().Single(o => o.Id == orderViewModel.Id);
                order.Address = orderViewModel.Address;
                order.FirstName = orderViewModel.FirstName;
                order.LastName = orderViewModel.LastName;
                order.Description = orderViewModel.Description;
                
                await _orderRepository.Save();
                
                return RedirectToAction("Index");
            }

            return View(orderViewModel);
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

        [NonAction]
        private async Task SetOrderCartItemsAndTotalPrice(OrderViewModel order)
        {
            var cartItemModels = await _cartService.GetAll();
            var cartItemViewModels = _mapper.Map<List<ShoppingCartItemViewModel>>(cartItemModels);

            order.CartItems = cartItemViewModels;
            order.TotalPrice = _cartService.GetTotalPrice(cartItemModels);
        }
    }
}