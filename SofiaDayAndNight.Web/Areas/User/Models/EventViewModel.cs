using System;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Web.Models;

namespace SofiaDayAndNight.Web.Areas.User
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public int AgeRestriction { get; set; } // if not = 0

        public Privacy Privacy { get; set; }

        public DateTime Begins { get; set; }

        public DateTime Ends { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public EventType EventType { get; set; }

        public ImageViewModel Cover { get; set; }

        //public IEnumerable<CommentViewModel> Comments { get; set; }

        //public IEnumerable<UserPersonalInfo> AttendedBy { get; set; }

        //public IEnumerable<UserPersonalInfo> AttendingBy { get; set; }

    }
}