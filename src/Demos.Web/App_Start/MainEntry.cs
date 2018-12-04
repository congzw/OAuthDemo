using System.Web.Mvc;
using System.Web.Routing;

namespace Demos.Web
{
    public class MainEntry
    {
        public static void Init()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
