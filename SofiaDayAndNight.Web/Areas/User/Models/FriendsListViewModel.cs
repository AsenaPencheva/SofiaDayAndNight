using System.Collections.Generic;

using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class FriendsListViewModel : IMapFrom<Individual>
    {
        public FriendsListViewModel()
        {
            this.Friends = new HashSet<IndividualViewModel>();
        }
        public string Username { get; set; }

        public IEnumerable<IndividualViewModel> Friends { get; set; }
    }
}