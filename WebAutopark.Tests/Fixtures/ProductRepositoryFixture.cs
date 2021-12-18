using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    public class ProductRepositoryFixture : RepositoryFixture<ProductRepository>
    {
        public int Id { get; set; }
        protected override ProductRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var products = new List<Product>
            {
                new () { Description = "Name" },
                new () { Description = "Name2" } 
            };
            
            await Connection.Products.AddRangeAsync(products);
            await Connection.SaveChangesAsync();
            
            Id = Connection.Products.First().Id;
        }
    }
}