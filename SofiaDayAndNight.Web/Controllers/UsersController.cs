using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult UserDetails(string username)
        {
            //service getUserByName(username)
            return View();
        }

        public ActionResult EventDetails(string username, int id)
        {
            //service getEventById(id)
            return View();
        }
    }
}