using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.Infrastructure;

namespace Fanex.Affiliate.User.Site
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add(new HandleLegacyUrlRoute(new Dictionary<string, UrlSchema> {
                {
                    @"/default.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/index.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/programdetails.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/condition.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/faqs.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/contactus.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/forgetpass.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
                {
                    @"/signup.aspx",new UrlSchema{Controller="Home",Action="Index"}
                },
            }));
            routes.MapRoute(
              name: "UserPanel",
              url: "UserPanel/{controller}/{action}/{id}",
              defaults: new { controller = "AccountOverview", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
