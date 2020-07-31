using System.Web.Mvc;

namespace Affiliate.ThirdPartyProduct.Areas.ThirdPartyProduct
{
    public class ThirdPartyProductAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Portal";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "ThirdPartyProduct",
                url: "ThirdPartyProduct/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fanex.Affiliate.ThirdPartyProduct.Controllers" }
            );
        }
    }
}