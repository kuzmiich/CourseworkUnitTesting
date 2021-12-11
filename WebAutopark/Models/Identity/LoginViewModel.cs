using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Wrong email size")]
        [Display(Name = "Email")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Wrong password size")]
        public string Password { get; set; }
    }
}