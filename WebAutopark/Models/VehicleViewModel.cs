using System;
using System.ComponentModel.DataAnnotations;
using WebAutopark.Core.Enums;

namespace WebAutopark.Models
{
    public class VehicleViewModel
    {
        private const double WeightCoefficient = 0.0013d;
        private const double ShiftForTax = 5d;
        private const double TaxCoefficient = 30d;

        #region Vehicle Property

        public int Id { get; set; }
        
        [Required] 
        public decimal Price { get; set; }
        
        [Required] 
        public uint ProductAmount { get; set; }
        
        [Required] 
        public int VehicleTypeId { get; set; }

        public VehicleTypeViewModel VehicleType { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 5)] 
        public string ModelName { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 5)] 
        public string RegistrationNumber { get; set; }
        [Required]
        [Range(1980, int.MaxValue)] 
        public int ManufactureYear { get; set; }
        [Required]
        [Range(0, int.MaxValue)] 
        public int Weight { get; set; }
        [Required]
        [Range(0, int.MaxValue)] 
        public int Mileage { get; set; }
        [Required]
        [Range(1, 8)] 
        public ColorType Color { get; set; }

        #endregion
    }
}