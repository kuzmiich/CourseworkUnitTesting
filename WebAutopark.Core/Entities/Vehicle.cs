using System;
using System.Collections.Generic;
using System.Drawing;
using WebAutopark.Core.Enums;

namespace WebAutopark.Core.Entities
{
    public class Vehicle
    {
        private const double WeightCoefficient = 0.0013d;
        private const double ShiftForTax = 5d;
        private const double TaxCoefficient = 30d;
        
        #region Vehicle Property

        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public string ModelName { get; set; }
        public VehicleType VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public int ManufactureYear { get; set; }
        public int Weight { get; set; }
        public int Mileage { get; set; }
        public ColorType Color { get; set; }
        public double EngineConsumption { get; set; }
        public double TankCapacity { get; set; }

        public virtual double GetCalcTaxPerMonth => (VehicleType is not null) ? Math.Round(Weight * WeightCoefficient + VehicleType.TaxCoefficient * TaxCoefficient + ShiftForTax, 2) : 0d;
        
        public virtual double KmPerFullTank => (TankCapacity != 0 && EngineConsumption != 0) ? Math.Round(TankCapacity / EngineConsumption, 2) : 0d;

        #endregion
    }
}
