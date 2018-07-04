using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Filters;

namespace Fanex.Affiliate.User.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalErrorHandlerAttribute());
        }
    }
}
