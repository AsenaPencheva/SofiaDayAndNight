using System.Collections.Generic;

using SofiaDayAndNight.Common.Enums;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class FriendsListViewModel
    {
        public FriendsListViewModel()
        {
            this.FriendsConnectionList = new HashSet<IndividualViewModel>();
        }

        public string UserName { get; set; }

        public IndividualStatus IndividualStatus { get; set; }

        public HashSet<IndividualViewModel> FriendsConnectionList { get; private set; }
    }
}