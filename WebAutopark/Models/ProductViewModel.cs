using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class ProductViewModel
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}