using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult EventDetails(string username,int id)
        {
           
            // service getEventById(name)
            // event.username==username 
            return View();
        }


        public ActionResult AllEvents(string username)
        {
            // var user=getUserByUsername(username)
            // var userId=user.Id

            return View();
        }
    }
}