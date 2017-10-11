using SofiaDayAndNight.Web.Areas.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SofiaDayAndNight.Web.Models
{
    public class ResultViewModel
    {
        public FriendsListViewModel IndividualsList { get; set; }

        public OrganizationsListViewModel OrganizationsList { get; set; }

        public EventsListViewModel EventsList { get; set; }
    }
}