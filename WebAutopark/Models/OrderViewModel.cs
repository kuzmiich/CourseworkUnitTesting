using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public List<Product> Products { get; set; }
        
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        
        public string Description { get; set; }
    }
}