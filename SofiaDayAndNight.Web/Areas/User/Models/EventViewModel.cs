using System;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Web.Models;
using System.ComponentModel.DataAnnotations;
using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Data.Models;
using AutoMapper;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class EventViewModel : IMapFrom<Event>
    {
        public EventViewModel()
        {
            this.Id = Guid.NewGuid();
            this.Begins = DateTime.Now;
            this.Ends = DateTime.Now;
        }
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public string creatorUserName { get; set; }

        //public int AgeRestriction { get; set; } // if not = 0

        public Privacy Privacy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Begins { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ends { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public EventType EventType { get; set; }

        public ImageViewModel Cover { get; set; }

        //public ImageViewModel Creator { get; set; }

        //public IEnumerable<CommentViewModel> Comments { get; set; }

        //public IEnumerable<UserPersonalInfo> AttendedBy { get; set; }

        //public IEnumerable<UserPersonalInfo> AttendingBy { get; set; }
    }
}