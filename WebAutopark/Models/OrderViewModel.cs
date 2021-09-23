using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;

namespace WebAutopark.BusinessLogic.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required] public int VehicleId { get; set; }

        public VehicleViewModel Vehicle { get; set; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
