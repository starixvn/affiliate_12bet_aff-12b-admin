namespace Affiliate.Banner.App_Start
{
    using Fanex.Affiliate.Banner.Core;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route Config.
    /// </summary>
    public static class RouteConfig
    {
        private const string ImageFolder = "CategoryMedia";

        /// <summary>
        /// Register Routes.
        /// </summary>
        /// <param name="routes">Route Collection.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new PatchImageUrlRoute(ImageFolder, new List<string> { ".gif", ".png", ".jpg", ".jpeg" }));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional });
        }
    }
}