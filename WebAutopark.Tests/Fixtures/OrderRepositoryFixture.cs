using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    internal class OrderRepositoryFixture : RepositoryFixture<OrderRepository>
    {
        public int Id { get; set; }
        protected override OrderRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var orders = new List<Order>
            {
                new () { Description = "Name" },
                new () { Description = "Name2" } 
            };
            
            await Connection.Orders.AddRangeAsync(orders);
            await Connection.SaveChangesAsync();
            
            Id = Connection.Orders.First().Id;
        }
    }
}