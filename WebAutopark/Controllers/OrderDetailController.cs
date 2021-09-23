using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.BusinessLogic.Models;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailController(IRepository<OrderDetail> orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailCreate(OrderViewModel order)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetailCreate(OrderDetailViewModel orderDetail)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailRepository.Create(_mapper.Map<OrderDetail>(orderDetail));

                return RedirectToAction("OrderInfo", "Order", new {id = orderDetail.OrderId});
            }

            return View();
        }
    }
}