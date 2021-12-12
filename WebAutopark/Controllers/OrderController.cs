using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Constants;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.User)]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAll();

            return View(_mapper.Map<IEnumerable<OrderViewModel>>(orders));
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.User)]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> OrderInfo(int id)
        {
            var order = await _orderService.GetById(id);

            return View(_mapper.Map<OrderViewModel>(order));
        }
        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.User)]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public IActionResult OrderCreate()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.User)]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> OrderCreate(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Create(_mapper.Map<OrderModel>(order));
                return RedirectToAction("Index");
            }

            return View();
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