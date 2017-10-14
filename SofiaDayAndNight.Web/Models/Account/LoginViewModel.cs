using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Web.Models.Account
{
    public class LoginViewModel
    {
        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}