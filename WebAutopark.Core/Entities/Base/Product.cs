namespace WebAutopark.Core.Entities.Base
{
    public abstract class Product : Entity
    {
        public abstract string Type { get; }
        
        public decimal Price { get; set; }
        
        public uint ProductAmount { get; set; }
    }
}