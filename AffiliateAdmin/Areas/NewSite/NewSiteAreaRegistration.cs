using System.Web.Mvc;

namespace AffiliateAdmin.Areas.NewSite
{
    public class NewSiteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "NewSite";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "NewSite",
                url: "NewSite/{controller}/{action}"
            );
        }
    }
}