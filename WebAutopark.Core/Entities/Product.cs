using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Core.Entities
{
    public class Product : Entity
    {
        public string ImgUrl { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}