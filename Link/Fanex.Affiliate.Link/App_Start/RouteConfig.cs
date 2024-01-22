namespace Fanex.Affiliate.Link
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route configuration.
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// Register Routes.
        /// </summary>
        /// <param name="routes">Route Collection.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "LinkNoLanguage",
               url: "{affId}/{pageCode}/index.html",
               defaults:
               new
               {
                   controller = "Home",
                   action = "Link",
                   affId = UrlParameter.Optional,
                   pageCode = UrlParameter.Optional
               },
               constraints:
               new
               {
                   affId = @"\d+"
               });

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
                   pageCode = UrlParameter.Optional,
                   lang = UrlParameter.Optional
               },
               constraints:
               new
               {
                   affId = @"\d+"
               });

            routes.MapRoute(
                name: "CommonLink",
                url: "{affId}/{pageCode}/{lang}/{targetCountry}/index.html", //May be "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}"
                defaults:
                new
                {
                    controller = "Home",
                    action = "CommonLink",
                    affId = UrlParameter.Optional,
                    pageCode = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    targetCountry = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affId = @"\d+"
                });

            routes.MapRoute(
                name: "SpecialPageCodeLink",
                url: "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}/{targetCountry}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "SpecialPageCodeLink",
                    affId = UrlParameter.Optional,
                    pageCodePart1 = UrlParameter.Optional,
                    pageCodePart2 = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    targetCountry = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affId = @"\d+"
                });

            routes.IgnoreRoute("go");
        }
    }
}