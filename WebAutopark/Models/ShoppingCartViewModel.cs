using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAutopark.Models
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItemViewModel> ShoppingCarts { get; set; }

        public decimal TotalPrice => ShoppingCarts.Sum(product => product.Product.Price * product.Amount);
    }
}