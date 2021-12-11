using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public List<Product> Products { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        public string Description { get; set; }
    }
}