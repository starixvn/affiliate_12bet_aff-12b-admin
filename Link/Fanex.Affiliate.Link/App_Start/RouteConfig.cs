namespace Fanex.Affiliate.Link
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route configuration.
    /// </summary>
    public static class RouteConfig
    {
        private const string AFF_ID_CONSTRAINT = @"\d+";
        private const string AFF_CODE_CONSTRAINT = @"\d+";
        private const string LANGUAGE_CONSTRAINT = @"(\w{2}|hien|hitg|eneuro|csmy)";

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
                name: "OldLink",
                url: "{affId}/{pageCode}/{lang}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "OldLink",
                    affId = UrlParameter.Optional,
                    pageCode = UrlParameter.Optional,
                    lang = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affId = AFF_ID_CONSTRAINT,
                    lang = LANGUAGE_CONSTRAINT
                });

            routes.MapRoute(
                name: "SpecialPageCodeOldLink",
                url: "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "SpecialPageCodeOldLink",
                    affId = UrlParameter.Optional,
                    pageCodePart1 = UrlParameter.Optional,
                    pageCodePart2 = UrlParameter.Optional,
                    lang = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affId = AFF_ID_CONSTRAINT,
                    lang = LANGUAGE_CONSTRAINT
                });

            routes.MapRoute(
                name: "ChannelLink",
                url: "{pageCode}/{affCode}/{lang}/{channel}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "Link",
                    pageCode = UrlParameter.Optional,
                    affCode = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    channel = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affCode = AFF_CODE_CONSTRAINT,
                    lang = LANGUAGE_CONSTRAINT
                });

            routes.MapRoute(
                name: "SpecialPageCodeChannelLink",
                url: "{pageCodePart1}/{pageCodePart2}/{affCode}/{lang}/{channel}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "SpecialPageCodeLink",
                    pageCodePart1 = UrlParameter.Optional,
                    pageCodePart2 = UrlParameter.Optional,
                    affCode = UrlParameter.Optional,
                    lang = UrlParameter.Optional,
                    channel = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affCode = AFF_CODE_CONSTRAINT,
                    lang = LANGUAGE_CONSTRAINT
                });

            routes.MapRoute(
                name: "LinkWithoutChannel",
                url: "{pageCode}/{affCode}/{lang}/index.html",
                defaults:
                new
                {
                    controller = "Home",
                    action = "Link",
                    pageCode = UrlParameter.Optional,
                    affCode = UrlParameter.Optional,
                    lang = UrlParameter.Optional
                },
                constraints:
                new
                {
                    affCode = AFF_CODE_CONSTRAINT,
                    lang = LANGUAGE_CONSTRAINT
                });

            routes.IgnoreRoute("go");
        }
    }
}