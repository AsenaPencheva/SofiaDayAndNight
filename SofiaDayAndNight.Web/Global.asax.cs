using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using SofiaDayAndNight.Data;
using SofiaDayAndNight.Data.Migrations;
using SofiaDayAndNight.Web.App_Start;

namespace SofiaDayAndNight.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SofiaDayAndNightDbContext, Configuration>());

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
