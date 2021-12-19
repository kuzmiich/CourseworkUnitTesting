using System;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name must contain from 4 to 50 characters")]
        public string Name { get; set; }
        
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be more then 0")]
        public decimal Price { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}