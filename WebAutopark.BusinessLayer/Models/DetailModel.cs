using System;

namespace WebAutopark.BusinessLayer.Models
{
    public class DetailModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public uint ProductAmount { get; set; }
    }
}