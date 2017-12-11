using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectManagement.Api.Http
{
    /// <summary>
    /// Configure the default routes for the MVC part
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register the routes to the collection of MVC routes served by IIS
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}