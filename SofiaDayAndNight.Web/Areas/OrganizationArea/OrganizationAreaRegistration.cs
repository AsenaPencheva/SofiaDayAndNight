using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.OrganizationArea
{
    public class OrganizationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OrganizationArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OrganizationArea_default",
                "OrganizationArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}