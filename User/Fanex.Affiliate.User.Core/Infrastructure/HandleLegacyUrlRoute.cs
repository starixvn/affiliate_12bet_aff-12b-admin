using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Affiliate.User.Core.Infrastructure
{
    public class HandleLegacyUrlRoute : RouteBase
    {
        private readonly IDictionary<string, UrlSchema> urlMap;

        public HandleLegacyUrlRoute(IDictionary<string, UrlSchema> urlMap)
        {
            this.urlMap = urlMap ?? throw new ArgumentNullException(nameof(urlMap));
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var path = httpContext.Request.Path.ToLowerInvariant();
            if (path.EndsWith(".aspx"))
            {
                var item = urlMap.Where(i => path.Contains(i.Key)).DefaultIfEmpty(urlMap.First())
                    .First().Value;

                var routeData = new RouteData(this, new MvcRouteHandler())
                {
                    Values =
                    {
                        ["controller"] = item.Controller,
                        ["action"] = item.Action
                    }
                };

                return routeData;
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}