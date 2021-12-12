using System;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "")]
        [StringLength(30, MinimumLength = 5)] 
        public string TypeName { get; set; }

        [Range(1d, double.MaxValue)] 
        public double TaxCoefficient { get; set; }
    }
}