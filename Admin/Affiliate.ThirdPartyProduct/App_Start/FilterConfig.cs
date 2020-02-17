using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
