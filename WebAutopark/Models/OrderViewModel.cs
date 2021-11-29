using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required] public int DetailId { get; set; }

        public DetailViewModel Detail { get; set; }

        [Range(1, int.MaxValue)] public int DetailAmount { get; set; }
    }
}