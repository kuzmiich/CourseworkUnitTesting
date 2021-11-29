namespace WebAutopark.Core.Entities.Base
{
    public class Product : Entity
    {
        public virtual string Type { get; }
        
        public decimal Price { get; set; }
        
        public uint ProductAmount { get; set; }
    }
}