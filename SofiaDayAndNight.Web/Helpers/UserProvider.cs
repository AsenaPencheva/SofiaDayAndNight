using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Web.Helpers
{
    public class UserProvider : IUserProvider
    {
        public User FindByName(string username)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName(username);
        }

        public void Update(User user)
        {
            HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Update(user);
        }
    }
}