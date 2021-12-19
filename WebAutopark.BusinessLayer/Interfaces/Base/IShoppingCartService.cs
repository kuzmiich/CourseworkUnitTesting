using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAutopark.BusinessLayer.Models;

namespace WebAutopark.BusinessLayer.Interfaces.Base
{
    public interface IShoppingCartService<TModel> : IDisposable, IAsyncDisposable
    {
        Task<List<TModel>> GetAll();

        Task<TModel> AddProduct(ProductModel productModel, int amount);

        Task<TModel> RemoveProduct(ProductModel product);

        Task ClearCart();
    }
}