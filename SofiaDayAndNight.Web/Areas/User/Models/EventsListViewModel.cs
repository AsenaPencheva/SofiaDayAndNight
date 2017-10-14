using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class EventsListViewModel
    {
        public EventsListViewModel()
        {
            this.PassedEvents = new HashSet<EventViewModel>();
            this.OngoingEvents = new HashSet<EventViewModel>();
            this.UpCommingEvents = new HashSet<EventViewModel>();
        }
        public string Username { get; set; }

        public IEnumerable<EventViewModel> PassedEvents{ get;set; }

        public IEnumerable<EventViewModel> OngoingEvents { get; set; }

        public IEnumerable<EventViewModel> UpCommingEvents { get; set; }
    }
}