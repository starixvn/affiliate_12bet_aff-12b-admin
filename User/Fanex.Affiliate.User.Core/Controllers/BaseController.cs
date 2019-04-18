using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CheckUMFilter]
    public class BaseController : Controller
    {
        protected readonly ILanguageService LanguageService;

        protected BaseController(ISessionService sessionService,
            ILanguageService languageService)
        {
            SessionService = sessionService;
            LanguageService = languageService;
        }

        protected ISessionService SessionService { get; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var favoriteLanguage = LanguageService.GetFavoriteLanguage();
            var sessionLang = SessionService.CurrentLanguage;
            if (favoriteLanguage != sessionLang)
            {
                requestContext.HttpContext.Response.Cookies.Add(new System.Web.HttpCookie("afffavoritelang", favoriteLanguage) { HttpOnly = true });
                SessionService.CurrentLanguage = favoriteLanguage;
            }
            LanguageService.SetCulture(favoriteLanguage);
        }

    }
}
