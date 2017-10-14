using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.Individuals = new HashSet<IndividualViewModel>();
            this.Organizations = new HashSet<OrganizationViewModel>();
            this.Events = new HashSet<EventViewModel>();
        }

        public IEnumerable<IndividualViewModel> Individuals { get; set; }

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }
    }
}