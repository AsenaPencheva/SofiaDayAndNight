using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class OrganizationsListViewModel
    {
        public OrganizationsListViewModel()
        {
            this.Organizations = new HashSet<OrganizationViewModel>();
        }
        public string Username { get; set; }

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }
    }
}