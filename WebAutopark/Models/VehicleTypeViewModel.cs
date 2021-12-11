using System;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class VehicleTypeViewModel
    {
        public Guid Id { get; set; }

        [StringLength(30, MinimumLength = 5)] 
        public string TypeName { get; set; }

        [Range(1d, double.MaxValue)] 
        public double TaxCoefficient { get; set; }
    }
}