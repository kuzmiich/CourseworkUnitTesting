using System.Collections.Generic;
using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Core.Entities
{
    public class Order : Entity
    {
        public IEnumerable<Product> Products { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}