using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;

namespace WebAutopark.BusinessLayer.Services
{
    public class ShoppingShoppingCartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ShoppingCartRepository _repository;

        public ShoppingShoppingCartService(ShoppingCartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public decimal GetTotalPrice(IEnumerable<ShoppingCartItemModel> cartItems) =>
            cartItems.Sum(item => item.Product.Price * item.Amount);

        public async Task SetCartItemsAndTotalPrice(OrderModel order)
        {
            var cartItems = await GetAll();
            var cartItemModels = _mapper.Map<List<ShoppingCartItemModel>>(cartItems);

            order.CartItems = cartItemModels;
            order.TotalPrice = GetTotalPrice(cartItemModels);
        }

        public virtual async Task<List<ShoppingCartItemModel>> GetAll()
        {
            var entities = await _repository.GetAll()
                .Where(cartItem => cartItem.ShoppingCartId == _repository.ShoppingCartId)
                .Include(s => s.Product)
                .ToListAsync();

            return _mapper.Map<List<ShoppingCartItemModel>>(entities);
        }

        public async Task<ShoppingCartItemModel> AddProduct(ProductModel productModel, int amount)
        {
            var product = _mapper.Map<Product>(productModel);

            var createdEntity = await _repository.AddProduct(product, amount);
            await _repository.SaveChanges();

            return _mapper.Map<ShoppingCartItemModel>(createdEntity);
        }

        public async Task<ShoppingCartItemModel> RemoveProduct(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            
            var removedProduct = await _repository.RemoveProduct(product);
            await _repository.SaveChanges();

            return _mapper.Map<ShoppingCartItemModel>(removedProduct);
        }

        public async Task ClearCart()
        {
            await _repository.ClearCart();
            await _repository.SaveChanges();
        }

        public void Dispose() => _repository?.Dispose();

        public ValueTask DisposeAsync() => _repository.DisposeAsync();
    }
}