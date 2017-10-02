using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.IndividualArea
{
    public class IndividualAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IndividualArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IndividualArea_default",
                "IndividualArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}