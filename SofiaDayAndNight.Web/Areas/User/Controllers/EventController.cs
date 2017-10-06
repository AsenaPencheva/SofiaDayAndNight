using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    public class EventController : Controller
    {
        // GET: User/Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventsList(string username)
        {
            return View();
        }
    }
}