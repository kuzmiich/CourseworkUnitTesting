using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class ShoppingCartItemViewModel
    {
        public ProductViewModel Product { get; set; }
        
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}