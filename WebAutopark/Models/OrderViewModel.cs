using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Range(1, int.MaxValue)] 
        public int Amount { get; set; }
    }
}