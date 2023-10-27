namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO;
    using Fanex.Affiliate.User.Core.DTO.Auth;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.Resources;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.UserPanel.Marketing;

    public sealed class MediaGalleryController : BaseMarketingController
    {
        public MediaGalleryController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService languageService,
            IMarketingService marketingService
            ) : base(sessionService, appSetting, languageService, marketingService)
        {
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult Index()
        {
            var userSession = SessionService.UserSession;
            var searchForm = GetSearchFormViewModel(userSession);

            var selectedDomain = searchForm.DomainList.FirstOrDefault();
            var selectedPage = searchForm.MemberSitePageList.FirstOrDefault()?.Value ?? string.Empty;
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

            var viewModel = new MediaGalleryViewModel
            {
                SearchForm = searchForm,
                MarketingDataList = GetBannerList(bannerParam)
            };

            ViewBag.TotalItem = bannerParam.Nrows;

            return View("~/Views/UserPanel/AccountOverview/MediaGallery/Index.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetBanner(MarketingBannerParam bannerParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = MarketingService
                .GetDomainList(userSession.AffId, SessionService.IsInternal)
                .FirstOrDefault(d => d.DomainId == bannerParam.SelectedDomainId);

            var bannerSettingKey = userSession.PreferredLanguage.Replace("-", string.Empty).ToUpperInvariant();

            bannerParam.AffId = userSession.AffId;
            bannerParam.AffType = userSession.AffType;
            bannerParam.SelectedDomain = selectedDomain;
            bannerParam.TextLinkTemplate = AppSetting.TextLinkTemplate;
            bannerParam.BannerDomain = AppSetting.GetValue($"BannerDomain{bannerSettingKey}") ?? AppSetting.BannerDomain;

            var bannerList = GetBannerList(bannerParam);
            ViewBag.TotalItem = bannerParam.Nrows;

            return View("~/Views/UserPanel/AccountOverview/Partial/_BannerList.cshtml", bannerList);
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetSize(SizeParam param)
        {
            param.AffType = SessionService.UserSession.AffType;

            var sizeList = MarketingService.GetSizeList(param, SessionService.IsInternal);

            return Json(sizeList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult CountNumberOfCopies(CopiedBannerCounterParam param)
        {
            if (SessionService.IsInternal)
            {
                return Json(new ResultInfo { Message = Lang.ErrAuthorized });
            }

            param.AffId = SessionService.UserSession.AffId;
            var result = MarketingService.CountNumberOfCopies(param);

            return Json(result);
        }

        private IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam)
        {
            if (bannerParam.SelectedDomain == null)
            {
                return new List<Banner>();
            }

            return MarketingService.GetBannerList(bannerParam);
        }

        private SearchFormViewModel GetSearchFormViewModel(UserSession userSession)
        {
            var targetProductList = MarketingService.GetTargetProductList(SessionService.IsInternal);
            var sizeList = MarketingService.GetSizeList(new SizeParam { AffType = userSession.AffType, Language = userSession.PreferredLanguage }, SessionService.IsInternal);

            var linkModel = GetLinkFormViewModel();

            return new SearchFormViewModel
            {
                TargetCountryList = linkModel.TargetCountryList,
                LanguageList = linkModel.LanguageList,
                DomainList = linkModel.DomainList,
                MemberSitePageList  = linkModel.MemberSitePageList,
                TargetProductList = targetProductList.Select(i => new SelectListItem { Value = i.ProductId, Text = Lang.ResourceManager.GetString(i.ProductName ?? string.Empty) ?? i.ProductName }),
                SizeList = sizeList.Select(i => new SelectListItem { Value = i.Size, Text = i.Size })
            };
        }
    }
}