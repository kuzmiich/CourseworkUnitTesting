using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Core.Entities
{
    public class ShoppingCartItem : Entity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}