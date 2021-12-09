using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderDetailRepository, IMapper mapper)
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
        public async Task<IActionResult> OrderInfo(Guid id)
        {
            var order = await _orderRepository.GetById(id);

            return View(_mapper.Map<OrderViewModel>(order));
        }

        public IActionResult Create(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}