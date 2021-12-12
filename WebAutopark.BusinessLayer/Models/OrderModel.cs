using System;
using System.Collections.Generic;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.BusinessLayer.Models
{
    public class OrderModel
    {
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public int Id { get; set; }
        
        public List<Product> Products { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}