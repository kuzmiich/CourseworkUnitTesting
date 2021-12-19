using WebAutopark.Core.Entities.Base;

namespace WebAutopark.BusinessLayer.Models
{
    public class ShoppingCartItemModel : Entity
    {
        public int OrderId { get; set; }
        public ProductModel Product { get; set; }
        
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}