using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var entityEntry = await Connection.Orders.AddAsync(new Order { Description = "Name" });
            
            Id = entityEntry.Entity.Id;
            
            await Connection.Orders.AddRangeAsync(entityEntry.Entity);
            await Connection.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
        }
    }
}