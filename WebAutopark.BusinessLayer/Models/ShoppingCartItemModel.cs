using WebAutopark.Core.Entities.Base;

namespace WebAutopark.BusinessLayer.Models
{
    public class ShoppingCartItemModel : Entity
    {
        public ProductModel Product { get; set; }
        
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}