using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Web.Models.UserProfile;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Web.Models
{
    public class PlaceViewModel
    {
        public int Id { get; set; }

        [RegularExpression("^[\\s\\S]{3,50}$", ErrorMessage = "Name must be between 3 and 50 symbols")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        public string City { get; set; } // default Sofia

        public string Location { get; set; }

        public string ProfileImageUrl { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }

        public IEnumerable<UserPersonalInfo> FollowedBy { get; set; }
    }
}