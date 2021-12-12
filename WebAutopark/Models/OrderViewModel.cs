using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }

        public List<Product> Products { get; set; }
        
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        
        public string Description { get; set; }
    }
}