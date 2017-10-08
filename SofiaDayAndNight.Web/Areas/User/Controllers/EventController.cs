using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult EventsList(string username)
        {
            return View();
        }
    }
}