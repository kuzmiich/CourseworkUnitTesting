using System;
using System.ComponentModel.DataAnnotations;
using WebAutopark.Core.Enums;

namespace WebAutopark.BusinessLogic.Models
{
    public class VehicleViewModel
    {
        private const double WeightCoefficient = 0.0013d;
        private const double ShiftForTax = 5d;
        private const double TaxCoefficient = 30d;

        #region Vehicle Property

        public int VehicleId { get; set; }
        [Required] 
        public int VehicleTypeId { get; set; }

        public VehicleTypeViewModel VehicleType { get; set; }
        [MaxLength(30)] 
        public string ModelName { get; set; }
        [MaxLength(20)] 
        public string RegistrationNumber { get; set; }
        public int ManufactureYear { get; set; }
        [Range(0, int.MaxValue)]
        public int Weight { get; set; }
        [Range(0, int.MaxValue)]
        public int Mileage { get; set; }
        [Range(1, 8)]
        public ColorType Color { get; set; }
        [Range(0, double.MaxValue)]
        public double EngineConsumption { get; set; }
        [Range(0, double.MaxValue)]
        public double TankCapacity { get; set; }
        
        public virtual double GetCalcTaxPerMonth => (VehicleType is not null) ? Math.Round(Weight * WeightCoefficient + VehicleType.TaxCoefficient * TaxCoefficient + ShiftForTax, 2) : 0d;

        public virtual double KmPerFullTank => (TankCapacity != 0 || EngineConsumption != 0) ? Math.Round(TankCapacity / EngineConsumption, 2) : 0d;

        #endregion
    }
}
