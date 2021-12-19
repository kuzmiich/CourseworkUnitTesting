using System.Collections.Generic;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.BusinessLayer.Models
{
    public class OrderModel
    {
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ShoppingCartItemModel> CartItems { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Address { get; set; }

        public string Description { get; set; }
    }
}