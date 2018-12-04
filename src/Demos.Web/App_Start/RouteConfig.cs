using System.Web.Mvc;
using System.Web.Routing;

namespace Demos.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { typeof(RouteConfig).Namespace + ".Controllers" }
            );
            
            routes.MapRoute(
                name: "Root_Default",
                url: "",
                defaults: new {controller = "Home", action = "Index" },
                namespaces: new[] { typeof(RouteConfig).Namespace + ".Controllers" }
            );
        }
    }
}
