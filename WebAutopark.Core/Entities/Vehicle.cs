using System;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Enums;

namespace WebAutopark.Core.Entities
{
    public class Vehicle : Product
    {
        private const double WeightCoefficient = 0.0013d;
        private const double ShiftForTax = 5d;
        private const double TaxCoefficient = 30d;

        #region Vehicle Property

        public override string Type => "Vehicle";
        public int VehicleTypeId { get; set; }
        public string ModelName { get; set; }
        public VehicleType VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public int ManufactureYear { get; set; }
        public int Weight { get; set; }
        public int Mileage { get; set; }
        public ColorType Color { get; set; }

        public virtual double GetCalcTaxPerMonth => (VehicleType is not null)
            ? Math.Round(Weight * WeightCoefficient + VehicleType.TaxCoefficient * TaxCoefficient + ShiftForTax, 2)
            : 0d;

        #endregion
    }
}