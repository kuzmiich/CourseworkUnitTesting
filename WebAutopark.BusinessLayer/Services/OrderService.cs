using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services
{
    public class OrderService : Service<OrderModel, Order, IRepository<Order>>, IOrderService
    {
        public OrderService(IRepository<Order> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }

        public override async Task<List<OrderModel>> GetAll()
        {
            var entities = await Repository.GetAll()
                .Include(order => order.CartItems)
                .ThenInclude(item => item.Product)
                .ToListAsync();

            return Mapper.Map<List<OrderModel>>(entities);
        }
    }
}