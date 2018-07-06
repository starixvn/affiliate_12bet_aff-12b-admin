using System.Web.Mvc;

namespace AffiliateAdmin.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Portal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Portal",
                url: "Portal/{controller}/{action}"
            );
        }
    }
}