using System.ComponentModel.DataAnnotations;

namespace WebAutopark.Models.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Неверный размер")]
        public string Password { get; set; }

        [Display(Name = "Remember?")] 
        public bool RememberMe { get; set; }

        
    }
}