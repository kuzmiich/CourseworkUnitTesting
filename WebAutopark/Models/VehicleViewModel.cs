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

        public Guid Id { get; set; }
        [Required] 
        public int VehicleTypeId { get; set; }

        public VehicleTypeViewModel VehicleType { get; set; }
        [StringLength(30, MinimumLength = 5)] 
        public string ModelName { get; set; }
        [StringLength(30, MinimumLength = 5)] 
        public string RegistrationNumber { get; set; }
        [Range(1980, int.MaxValue)] 
        public int ManufactureYear { get; set; }
        [Range(0, int.MaxValue)] 
        public int Weight { get; set; }
        [Range(0, int.MaxValue)] 
        public int Mileage { get; set; }
        [Range(1, 8)] 
        public ColorType Color { get; set; }
        [Range(0, double.MaxValue)] 
        

        public virtual double GetCalcTaxPerMonth => (VehicleType is not null)
            ? Math.Round(Weight * WeightCoefficient +
                         VehicleType.TaxCoefficient * TaxCoefficient + ShiftForTax, 2)
            : 0d;

        #endregion
    }
}