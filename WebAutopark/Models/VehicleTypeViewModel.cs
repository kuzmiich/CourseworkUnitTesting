using System;
using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Type name is required field")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Type name must contain from 5 to 30 characters")] 
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Tax coefficient is required field")]
        [Range(1d, double.MaxValue, ErrorMessage = "Tax coefficient should be more then 1")] 
        public double TaxCoefficient { get; set; }
    }
}