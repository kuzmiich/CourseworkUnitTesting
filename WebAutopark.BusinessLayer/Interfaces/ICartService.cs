using System.Collections.Generic;
using System.Threading.Tasks;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.BusinessLayer.Models;

namespace WebAutopark.BusinessLayer.Interfaces
{
    public interface ICartService : IShoppingCartService<ShoppingCartItemModel>
    {
        decimal GetTotalPrice(IEnumerable<ShoppingCartItemModel> cartItems);
        Task SetCartItemsAndTotalPrice(OrderModel order);
    }
}