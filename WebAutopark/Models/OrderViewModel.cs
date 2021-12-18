using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        [Key] 
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }

        public decimal Price { get; }
        
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Address must contain from 5 to 30 characters")]
        public string Address { get; set; }
        
        public string Description { get; set; }
        
        public List<ShoppingCartItemViewModel> CartItems { get; set; }
    }
}