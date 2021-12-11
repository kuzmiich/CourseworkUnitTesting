using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class DetailViewModel
    {
        public int DetailId { get; set; }

        [StringLength(30, MinimumLength = 4)] 
        public string Name { get; set; }
    }
}