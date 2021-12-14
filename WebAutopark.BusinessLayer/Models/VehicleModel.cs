using System;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Enums;

namespace WebAutopark.BusinessLayer.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        
        public decimal Price { get; set; }
        
        public uint ProductAmount { get; set; }
        
        public int VehicleTypeId { get; set; }
        
        public string ModelName { get; set; }
        public VehicleType VehicleType { get; set; }
        
        public string RegistrationNumber { get; set; }
        
        public int ManufactureYear { get; set; }
        public int Weight { get; set; }
        
        public int Mileage { get; set; }
        
        public ColorType Color { get; set; }
    }
}