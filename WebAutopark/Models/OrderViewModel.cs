using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Address must contain from 5 to 30 characters")]
        public string Address { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "FirstName must contain from 5 to 30 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "LastName must contain from 5 to 30 characters")]

        public string LastName { get; set; }
        
        public string Description { get; set; }

        public decimal TotalPrice { get; set; }
        
        public List<ShoppingCartItemViewModel> CartItems { get; set; }
    }
}