using Fanex.Affiliate.User.Core.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Site
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalErrorHandlerAttribute());
        }
    }
}
