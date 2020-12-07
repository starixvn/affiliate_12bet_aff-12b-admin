namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Marketing;

    public sealed class MediaGalleryController : BaseMarketingController
    {
        public MediaGalleryController(ISessionService sessionService, IAppSetting appSetting,
            ILanguageService languageService,
            IMarketingService marketingService) :
            base(sessionService, appSetting, languageService, marketingService)
        {
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult Index()
        {
            var userSession = SessionService.UserSession;
            var searchForm = GetSearchFormViewModel(true);
            var linkForm = GetLinkFormViewModel();

            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.FirstOrDefault()?.PageCode ?? string.Empty;
            var bannerSettingKey = userSession.PreferredLanguage.Replace("-", string.Empty).ToUpperInvariant();

            var bannerParam = new MarketingBannerParam
            {
                AffId = userSession.AffId,
                Language = userSession.PreferredLanguage,
                AffType = userSession.AffType,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                TextLinkTemplate = AppSetting.TextLinkTemplate,
                BannerDomain = AppSetting.GetValue($"BannerDomain{bannerSettingKey}") ?? AppSetting.BannerDomain
            };

            var viewModel = new MarketingViewModel
            {
                SearchForm = searchForm,
                MarketingDataList = GetBannerList(bannerParam)
            };

            ViewBag.TotalItem = bannerParam.Nrows;

            return View("~/Views/UserPanel/AccountOverview/MediaGallery/Index.cshtml", viewModel);
        }
    }
}