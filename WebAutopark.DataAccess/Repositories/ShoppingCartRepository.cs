using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class ShoppingCartRepository : ICartRepository<ShoppingCartItem>
    {
        private readonly WebAutoparkContext _context;

        public ShoppingCartRepository(WebAutoparkContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }

        public IQueryable<ShoppingCartItem> GetAll() => _context.ShoppingCartItems.AsNoTracking();

        public async Task<ShoppingCartItem> AddProduct(Product product, int amount)
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                .SingleOrDefaultAsync(cartItem =>
                    cartItem.Product.Id == product.Id &&
                    cartItem.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem is null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    ProductId = product.Id,
                    Amount = 1
                };
                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            return shoppingCartItem;
        }

        public async Task<ShoppingCartItem> RemoveProduct(Product product)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.SingleOrDefaultAsync(
                item => item.Product.Id == product.Id && item.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem is null) 
                return null;
            
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            return shoppingCartItem;
        }

        public async Task ClearCart()
        {
            var cartItems = await _context.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId)
                .ToListAsync();

            _context.ShoppingCartItems.RemoveRange(cartItems);
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public ValueTask DisposeAsync() => _context.DisposeAsync();
    }
}