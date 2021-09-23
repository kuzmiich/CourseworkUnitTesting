using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models
{
    public class DetailViewModel
    {
        public int DetailId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
    }
}
