using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly IIpAddressService _ipAddressService;

        public HomeController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            IIpAddressService ipAddressService) : base(sessionService, languageService)

        {
            _appSetting = appSetting;
            _ipAddressService = ipAddressService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var isAuthenticated = Request.IsAuthenticated && SessionService.UserSession != null;
            var today = DateTime.Now;
            var isValidDate = DateTime.Compare(today, _appSetting.ShowingNoticeEndDate) < 0;

            var viewModel = new HomeViewModel
            {
                IsAuthenticated = isAuthenticated,
                IsShowAlert = isValidDate && (_appSetting.IsLocal || _ipAddressService.IsThaiLandIpAddress(RequestHelper.GetValidOrEmptyClientIPAddress(Request))),
                Language = SessionService.CurrentLanguage,
                SignUpLink = isAuthenticated ? Url.Content("~/UserPanel/Main/#AccountOverView") : Url.Content("~/SignUp"),
                BannerLink = isAuthenticated ? Url.Content("~/UserPanel/Main/#MediaGallery") : "javascript:;",
                Promotion12BetLink = GetPromotionLink()
            };

            return View(viewModel);
        }

        private string GetPromotionLink()
        {
            var segments = Request.Url.Host.Split('.');
            var domainName = segments.Length > 1 ? segments[segments.Length - 2] : string.Empty;

            return string.Format(_appSetting.Promotion12BetLink, domainName).Replace("..", ".");
        }
    }
}