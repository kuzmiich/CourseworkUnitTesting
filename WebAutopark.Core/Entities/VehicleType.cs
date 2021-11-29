using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Core.Entities
{
    public class VehicleType : Entity
    {
        public string TypeName { get; set; }
        public double TaxCoefficient { get; set; }
    }
}