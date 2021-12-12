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
        
        [Required(ErrorMessage = "Price is required field")] 
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Product amount is required field")] 
        public uint ProductAmount { get; set; }
        
        [Required(ErrorMessage = "VehicleId is required field")] 
        public int VehicleTypeId { get; set; }

        public VehicleTypeViewModel VehicleType { get; set; }
        
        [Required(ErrorMessage = "Model name is required field")] 
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Error, count of symbol must be more then 5 and less then 30")] 
        public string ModelName { get; set; }
        
        [Required(ErrorMessage = "Registration number is required field")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Error, count of symbol must be more then 5 and less then 30")] 
        public string RegistrationNumber { get; set; }
        [Required]
        [Range(1980, int.MaxValue, ErrorMessage = "Error, this registration number is less then 1980")] 
        public int ManufactureYear { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Error, this weight is less then 0")] 
        public int Weight { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Error, this mileage is less then 0")] 
        public int Mileage { get; set; }
        [Required]
        [Range(1, 8)] 
        public ColorType Color { get; set; }

        #endregion
    }
}