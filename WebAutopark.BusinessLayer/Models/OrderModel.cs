using System;
using System.Collections.Generic;
using WebAutopark.Core.Entities.Base;

namespace WebAutopark.BusinessLayer.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        
        public List<Product> Products { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}