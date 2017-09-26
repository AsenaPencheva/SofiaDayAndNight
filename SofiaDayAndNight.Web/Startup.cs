using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SofiaDayAndNight.Web.Startup))]
namespace SofiaDayAndNight.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
