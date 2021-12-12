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
        
        public IEnumerable<Product> Products { get; set; }

        public string Description { get; set; }

        public decimal Price => Products.Sum(p => p.Price * p.ProductAmount);
    }
}