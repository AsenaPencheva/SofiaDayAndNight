using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Web.Models.UserProfile;
using System;

namespace SofiaDayAndNight.Web.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserPersonalInfo Author { get; set; }
    }
}