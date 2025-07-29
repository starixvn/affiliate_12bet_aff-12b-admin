using Fanex.Affiliate.Link.Constants;

namespace Fanex.Affiliate.Link.Extensions
{
    /// <summary>
    /// Extension methods for configuring affiliate link routes.
    /// Provides a centralized way to manage route configuration and makes testing easier.
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// Maps all affiliate link routes using consistent constraints and proper ordering.
        /// Routes are ordered from most specific to least specific to prevent conflicts.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder</param>
        public static void MapAffiliateRoutes(this IEndpointRouteBuilder endpoints)
        {
            // 1. Most specific: Special page code with channel (e.g., "12sports/cricket/123/en/mobile/index.html")
            endpoints.MapControllerRoute(
                name: "SpecialPageCodeChannelLink",
                pattern: "{pageCodePart1}/{pageCodePart2}/{affCode}/{lang}/{channel}/index.html",
                defaults: new { controller = "Home", action = "SpecialPageCodeLink" },
                constraints: new
                {
                    affCode = RouteConstraints.AffiliateCodePattern,
                    lang = RouteConstraints.LanguagePattern,
                    pageCodePart1 = RouteConstraints.PageCodePattern,
                    pageCodePart2 = RouteConstraints.PageCodePattern
                });

            // 2. Special page code for legacy routes (e.g., "123/12sports/cricket/en/index.html")
            endpoints.MapControllerRoute(
                name: "SpecialPageCodeOldLink",
                pattern: "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}/index.html",
                defaults: new { controller = "Home", action = "SpecialPageCodeOldLink" },
                constraints: new
                {
                    affId = RouteConstraints.AffiliateIdPattern,
                    lang = RouteConstraints.LanguagePattern,
                    pageCodePart1 = RouteConstraints.PageCodePattern,
                    pageCodePart2 = RouteConstraints.PageCodePattern
                });

            // 3. Channel link with single page code (e.g., "home/123/en/mobile/index.html")
            endpoints.MapAffiliateMediaRoute(
                "ChannelLink",
                "{pageCode}/{affCode}/{lang}/{channel}/index.html");

            // 4. Link without channel (e.g., "home/123/en/index.html")
            endpoints.MapAffiliateMediaRoute(
                "LinkWithoutChannel",
                "{pageCode}/{affCode}/{lang}/index.html");

            // 5. Legacy route format - least specific (e.g., "123/home/en/index.html")
            endpoints.MapControllerRoute(
                name: "OldLink",
                pattern: "{affId}/{pageCode}/{lang}/index.html",
                defaults: new { controller = "Home", action = "OldLink" },
                constraints: new
                {
                    affId = RouteConstraints.AffiliateIdPattern,
                    lang = RouteConstraints.LanguagePattern,
                    pageCode = RouteConstraints.PageCodePattern
                });
        }

        /// <summary>
        /// Maps a single affiliate route with all possible constraint patterns.
        /// Helper method for creating affiliate routes with consistent constraints.
        /// Only constraints that match parameters in the pattern will be applied.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder</param>
        /// <param name="name">Route name</param>
        /// <param name="pattern">URL pattern</param>
        /// <param name="action">Controller action</param>
        /// <param name="controller">Controller name (defaults to "Home")</param>
        public static void MapAffiliateMediaRoute(
            this IEndpointRouteBuilder endpoints,
            string name,
            string pattern,
            string action = "Link",
            string controller = "Home")
        {
            endpoints.MapControllerRoute(
                name: name,
                pattern: pattern,
                defaults: new { controller, action },
                constraints: new
                {
                    affCode = RouteConstraints.AffiliateCodePattern,
                    pageCode = RouteConstraints.PageCodePattern,
                    lang = RouteConstraints.LanguagePattern
                });
        }
    }
}