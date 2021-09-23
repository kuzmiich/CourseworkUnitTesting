using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;

namespace WebAutopark.BusinessLogic.Models
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int DetailId { get; set; }

        public WebAutopark.Models.DetailViewModel Detail { get; set; }

        [Range(1, int.MaxValue)]
        public int DetailAmount { get; set; }
    }
}
