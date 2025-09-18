using System;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Constants;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class LanguageRouteCheckingAttribute : ActionFilterAttribute
    {
        private const string IndexAction = "Index";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var systemService = DependencyResolver.Current.GetService<ISystemService>();
            var preferredUrlLang = filterContext.RouteData.Values["lang"]?.ToString();
            var rawUrl = filterContext.HttpContext.Request.RawUrl;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (string.IsNullOrEmpty(preferredUrlLang) || SystemLanguage.SupportedFavoriteLanguages.ContainsKey(preferredUrlLang))
            {
                var favoriteShortedLanguage = systemService.GetFavoriteShortedLanguage();

                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var action = filterContext.RouteData.Values["action"].ToString();
                var queryString = filterContext.HttpContext.Request?.QueryString.ToString();

                var path = action.MatchWith(IndexAction)
                    ? $"~/{favoriteShortedLanguage}/{controllerName}"
                    : $"~/{favoriteShortedLanguage}/{controllerName}/{action}";

                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    path += $"?{queryString}";
                }

                var controller = (BaseController)filterContext.Controller;
                var targetUrl = controller.Url.Content(path);

                if (!rawUrl.Equals(targetUrl, StringComparison.OrdinalIgnoreCase))
                {
                    filterContext.Result = controller.RedirectToAbsolutePath(path);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
