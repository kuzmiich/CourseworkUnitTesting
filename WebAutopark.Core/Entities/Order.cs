using System.Collections.Generic;

namespace WebAutopark.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
