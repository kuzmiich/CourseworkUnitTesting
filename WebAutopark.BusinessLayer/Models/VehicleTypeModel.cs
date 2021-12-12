using System;

namespace WebAutopark.BusinessLayer.Models
{
    public class VehicleTypeModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public double TaxCoefficient { get; set; }
    }
}