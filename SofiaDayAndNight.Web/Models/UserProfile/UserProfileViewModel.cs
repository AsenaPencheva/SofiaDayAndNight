using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Models.UserProfile
{
    public class UserProfileViewModel
    {
        public IEnumerable<UserPersonalInfo> PersonalInfo { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }

        public IEnumerable<UserProfileViewModel> Friends { get; set; }

        public IEnumerable<PlaceViewModel> FollowingPlaces { get; set; }

        public IEnumerable<EventViewModel> Attended { get; set; }

        public IEnumerable<EventViewModel> Attending { get; set; }
    }
}