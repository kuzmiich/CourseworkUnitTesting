using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLogic.Models;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.DataAccess.Repositories.ExtendRepository;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderDetailRepository, IMapper mapper)
        {
            _orderRepository = orderDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAll();

            return View(_mapper.Map<IEnumerable<OrderViewModel>>(orders));
        }

        [HttpGet]
        public async Task<IActionResult> OrderInfo(int id)
        {
            var order = await _orderRepository.GetById(id);

            return View(_mapper.Map<OrderViewModel>(order));
        }

        [HttpPost]
        public async Task<IActionResult> OrderCreate(int id)
        {
            var insertOrder = await _orderRepository.CreateInsert(id);

            return RedirectToAction("OrderDetailCreate", "OrderDetail", insertOrder);
        }
    }
}