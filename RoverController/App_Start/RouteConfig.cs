using System.Web.Mvc;
using System.Web.Routing;

namespace RoverController
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Errors",
                url: "Errors/{action}/{id}",
                defaults: new { controller = "Errors", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}