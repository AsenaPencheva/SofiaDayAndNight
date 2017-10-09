using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class EventsListViewModel
    {
        public string Username { get; set; }

        public IEnumerable<EventViewModel> PassedEvents{ get;set; }

        public IEnumerable<EventViewModel> OngoingEvents { get; set; }

        public IEnumerable<EventViewModel> UpCommingEvents { get; set; }
    }
}