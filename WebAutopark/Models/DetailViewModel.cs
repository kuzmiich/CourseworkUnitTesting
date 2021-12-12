using System;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 4)] 
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public uint ProductAmount { get; set; }
    }
}