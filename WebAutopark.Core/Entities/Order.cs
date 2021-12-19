using System.Collections.Generic;
using System.Linq;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.Core.Entities
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public IEnumerable<ShoppingCartItem> CartItems { get; set; }
        
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}