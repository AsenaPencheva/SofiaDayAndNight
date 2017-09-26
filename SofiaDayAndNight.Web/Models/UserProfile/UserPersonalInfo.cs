using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Web.Models.UserProfile
{
    public class UserPersonalInfo
    {
        public int Id { get; set; }

        [RegularExpression("^[\\s\\S]{3,50}$", ErrorMessage = "First name must be between 3 and 50 symbols")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [RegularExpression("^[\\s\\S]{3,50}$", ErrorMessage = "Last name must be between 3 and 50 symbols")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}