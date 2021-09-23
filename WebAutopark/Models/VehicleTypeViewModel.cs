using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.BusinessLogic.Models
{
    public class VehicleTypeViewModel
    {
        public int VehicleTypeId { get; set; }

        [MaxLength(30)]
        public string TypeName { get; set; }

        [Range(1d, double.MaxValue)]
        public double TaxCoefficient { get; set; }
    }
}
