using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Web.Models.UserProfile;
using System;
using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int AgeRestriction { get; set; } // if not = 0

        //public Privacy Privacy { get; set; }

        public DateTime Begins { get; set; }

        public DateTime Ends { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        //public EventType EventType { get; set; }

        public  AlbumViewModel Album { get; set; }

        public PlaceViewModel Place { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<UserPersonalInfo> AttendedBy { get; set; }

        public IEnumerable<UserPersonalInfo> AttendingBy { get; set; }

    }
}