using System;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;

namespace WebAutopark.DataAccess.Repositories.Base
{
    public interface ICartRepository<TResult> : IDisposable, IAsyncDisposable
    {
        string ShoppingCartId { get; set; }
        
        IQueryable<TResult> GetAll();

        Task<TResult> AddProduct(Product product, int amount);

        Task<TResult> RemoveProduct(Product product);
        
        Task ClearCart();
        
        Task SaveChanges();
    }
}