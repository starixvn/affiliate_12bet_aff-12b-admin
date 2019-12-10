using Fanex.Affiliate.Admin.Core.Filter;
using System.Web;
using System.Web.Mvc;

namespace Affiliate.ThirdPartyProduct
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireSecureConnectionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
