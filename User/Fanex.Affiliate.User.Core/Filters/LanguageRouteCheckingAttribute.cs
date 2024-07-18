using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.Extensions;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class LanguageRouteCheckingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var preferedUrlLang = filterContext.RouteData.Values["lang"]?.ToString();

            if (!filterContext.HttpContext.Request.IsAjaxRequest() &&
                (string.IsNullOrEmpty(preferedUrlLang) || Regex.IsMatch(filterContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath, "~/((\\w{2}|hien|hitg)/?)?$")))
            {
                var systemService = DependencyResolver.Current.GetService<ISystemService>();
                var favoriteLanguage = systemService.GetFavoriteLanguage();
                var shortedLanguageCode = SystemLanguage.GetCurrentShortedLanguage(favoriteLanguage);

                var path = $"~/{shortedLanguageCode}/{filterContext.RouteData.Values["controller"]}";

                
                var controller = (BaseController)filterContext.Controller;
                filterContext.Result = controller.RedirectToAbsolutePath(path);
            }
        }
    }
}
