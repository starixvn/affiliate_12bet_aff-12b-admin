// ----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

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
                name: "LinkMobile",
                url: "{affId}/{pageId}/{lang}/{deviceType}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "LinkMobile",
                    affId = UrlParameter.Optional,
                    pageId = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    deviceType = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Link",
                url: "{affId}/{pageId}/{lang}/index.html",
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
                name: "TrackingLink",
                url: "{affId}/{trackId}/{pageId}/{lang}/{deviceType}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "LinkTracking",
                    affId = UrlParameter.Optional,
                    trackId = string.Empty,
                    pageId = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    deviceType = UrlParameter.Optional
                });
            
        }
    }
}