using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using ONEbook.UM.AspNetMVC.Filters;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CheckUMFilter]
    public class BaseController : Controller
    {
        protected readonly ISystemService SystemService;

        protected BaseController(
            ISessionService sessionService,
            ISystemService systemService)
        {
            SessionService = sessionService;
            SystemService = systemService;
        }

        protected ISessionService SessionService { get; }

        protected virtual string RouteName => Constants.RouteName.DefaultRoute;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var favoriteLanguage = SystemService.GetFavoriteLanguage();
            var sessionLang = SessionService.CurrentLanguage;
            if (favoriteLanguage != sessionLang)
            {
                requestContext.HttpContext.Response.Cookies.Add(new HttpCookie("afffavoritelang", favoriteLanguage) { HttpOnly = true });
                SessionService.CurrentLanguage = favoriteLanguage;
            }
            SystemService.SetCulture(favoriteLanguage);
        }

        protected RedirectToRouteResult RedirectToFixedAction(string actionName, object routeValue)
        {
            var routeDic = new RouteValueDictionary(routeValue)
            {
                { "controller", RouteData.Values["controller"] },
                { "action", actionName }
            };
            var result = RedirectToRoute(RouteName, routeDic);
            return result;
        }

        public RedirectResult RedirectToAbsolutePath(string path)
        {
            var absolutePath = VirtualPathUtility.ToAbsolute(path);
            return Redirect(absolutePath);
        }
    }
}
