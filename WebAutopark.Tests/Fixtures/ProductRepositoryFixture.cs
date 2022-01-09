using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    public class ProductRepositoryFixture : RepositoryFixture<ProductRepository>
    {
        protected override ProductRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var entityEntry = await Connection.Products.AddAsync(new Product { Description = "Name" });
            Id = entityEntry.Entity.Id;
            
            await Connection.Products.AddAsync(entityEntry.Entity);
            await Connection.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
        }
    }
}