using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Controllers
{
    public class PlacesController : Controller
    {
        public ActionResult PlaceDetails(string name)
        {
            //service getPlaceByName(name)
            return View();
        }
    }
}