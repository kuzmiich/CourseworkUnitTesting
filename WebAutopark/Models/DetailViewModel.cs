using System;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The type name of the specialty must contain from 5 to 30 characters")] 
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Price is required field")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Product amount is required field")]
        public uint ProductAmount { get; set; }
    }
}