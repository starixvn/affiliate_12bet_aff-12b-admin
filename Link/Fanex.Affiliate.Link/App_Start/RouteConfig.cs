namespace Fanex.Affiliate.Link
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route configuration.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register Routes.
        /// </summary>
        /// <param name="routes">Route Collection.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults:
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
               name: "Link",
               url: "{affId}/{pageCode}/{lang}/index.html",
               defaults:
               new
               {
                   controller = "Home",
                   action = "Link",
                   affId = UrlParameter.Optional,
                   pageId = UrlParameter.Optional,
                   lang = UrlParameter.Optional
               });

            routes.MapRoute(
                name: "NewLink",
                url: "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "NewLink",
                    affId = UrlParameter.Optional,
                    pageId = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    deviceType = UrlParameter.Optional
                });

            routes.IgnoreRoute("go");
        }
    }
}