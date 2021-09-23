using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.Core.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int DetailId { get; set; }

        public Detail Detail { get; set; }

        public int DetailAmount { get; set; }

    }
}
