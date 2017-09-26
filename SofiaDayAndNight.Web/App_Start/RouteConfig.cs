using System.Web.Mvc;
using System.Web.Routing;

namespace SofiaDayAndNight.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
              name: "UserEvents",
              url: "users/{username}/events",
              defaults: new { controller = "Events", action = "AllEvents" }
          );

            //  routes.MapRoute(
            //    name: "UserFriends",
            //    url: "users/{fullName}/friends",
            //    defaults: new { controller = "Users", action = "Friends" }
            //);

            //  routes.MapRoute(
            //    name: "UserPlaces",
            //    url: "users/{fullName}/places",
            //    defaults: new { controller = "Users", action = "Places" }
            //);

            routes.MapRoute(
              name: "UserEventDetails",
              url: "users/{username}/event/{id}",
              defaults: new { controller = "Events", action = "EventDetails" }
          );

            routes.MapRoute(
              name: "PlaceEvents",
              url: "places/{username}/events",
              defaults: new { controller = "Events", action = "AllEvents" }
          );

            routes.MapRoute(
              name: "PlaceEventDetails",
              url: "places/{username}/event/{id}",
              defaults: new { controller = "Events", action = "EventDetails" }
          );

            routes.MapRoute(
               name: "UserDetails",
               url: "users/{username}",
               defaults: new { controller = "Users", action = "UserDetails" }
           ); //ok

            routes.MapRoute(
              name: "PlaceDetails",
              url: "places/{name}",
              defaults: new { controller = "Places", action = "PlaceDetails" }
          );

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}
