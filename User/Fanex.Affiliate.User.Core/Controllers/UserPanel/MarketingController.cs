namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO.UserPanel;
    using Extensions;
    using Fanex.Affiliate.User.Core.Resources;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using System.Drawing;
    using ViewModels.UserPanel.Marketing;

    [CustomAuthorize]
    public class MarketingController : BaseMarketingController
    {

        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;

        public MarketingController(ISessionService sessionService, IAppSetting appSetting,
            ILanguageService languageService,
            IMarketingService marketingService,
            IQrCodeGeneratorService QrCodeGeneratorService) : base(sessionService, appSetting, languageService, marketingService)
        {
            _qrCodeGeneratorService = QrCodeGeneratorService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var userSession = SessionService.UserSession;
            var linkForm = GetLinkFormViewModel();
            var languageList = MarketingService.GetLanguageList(SessionService.IsInternal);

            var selectedLanguage = userSession.PreferredLanguage;
            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.FirstOrDefault()?.PageCode ?? string.Empty;
            var selectedLanguageList = MarketingHelper.GetLanguageList(languageList, SessionService.UserSession.PreferredLanguage);

            var linkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, selectedLanguageList);
            var qrCodes = GetQrCodes(linkList);

            var marketingViewModel = new MarketingViewModel
            {
                QrCodeList = qrCodes,
                LinkForm = linkForm
            };

            return View("~/Views/UserPanel/AccountOverview/Partial/_GeneratedQrCode.cshtml", marketingViewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSubCategory(string category)
        {
            var subCategoryList = MarketingService.GetCategoryList(true, SessionService.IsInternal)
                                                    .Where(c => c.ParentCategory.MatchWith(category) && !c.Category.MatchWith(c.ParentCategory));

            return Json(subCategoryList.Select(i => new SelectListItem { Value = i.Category, Text = Lang.ResourceManager.GetString(i.Category.Trim()) ?? i.Category }),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSize(SizeParam param)
        {
            param.AffType = SessionService.UserSession.AffType;
            param.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(param.Language))?.EnglishName;
            var sizeList = MarketingService.GetSizeList(param, SessionService.IsInternal);

            return Json(sizeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBanner(MarketingBannerParam bannerParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = MarketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == bannerParam.SelectedDomainId);
            if (!string.IsNullOrEmpty(bannerParam.SubCategory))
            {
                bannerParam.Category = bannerParam.SubCategory;
            }

            var bannerSettingKey = userSession.PreferredLanguage.Replace("-", string.Empty).ToUpperInvariant();

            bannerParam.AffId = userSession.AffId;
            bannerParam.AffType = userSession.AffType;
            bannerParam.SelectedDomain = selectedDomain;
            bannerParam.TextLinkTemplate = AppSetting.TextLinkTemplate;
            bannerParam.BannerDomain = AppSetting.GetValue($"BannerDomain{bannerSettingKey}") ??
                AppSetting.BannerDomain;
            bannerParam.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(bannerParam.Language))?.EnglishName;

            var bannerList = GetBannerList(bannerParam);
            ViewBag.TotalItem = bannerParam.Nrows;

            return View("~/Views/UserPanel/AccountOverview/Partial/_BannerList.cshtml", bannerList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetText(MarketingTextParam textParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = MarketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == textParam.SelectedDomainId);

            textParam.AffId = userSession.AffId;
            textParam.SelectedDomain = selectedDomain;
            textParam.TextLinkTemplate = AppSetting.TextLinkTemplate;
            textParam.ScriptUrl = AppSetting.MediaScriptUrl;
            textParam.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(textParam.Language))?.EnglishName;

            var textList = GetTextList(textParam);
            ViewBag.TotalItem = textParam.Nrows;

            return View("~/Views/UserPanel/AccountOverview/Partial/_TextList.cshtml", textList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GenerateLink(string selectedLanguage, int selectedDomainId, string selectedPage)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = MarketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == selectedDomainId);
            var languageList = string.IsNullOrEmpty(selectedLanguage) ?
                MarketingHelper.GetLanguageList(MarketingService.GetLanguageList(SessionService.IsInternal), userSession.PreferredLanguage) : null;

            var textLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, languageList);

            var qrCodeParamsDic = GetQrCodes(textLinkList);

            return View("~/Views/UserPanel/AccountOverview/Partial/_TextLink.cshtml", qrCodeParamsDic);
        }

        private IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            if (bannerParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return MarketingService.GetBannerList(bannerParam);
        }

        private IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            if (textParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return MarketingService.GetTextList(textParam);
        }

        private IDictionary<string, QrCodeLinkModel> GetQrCodes(IDictionary<string, string> txtLinkList)
        {
            var qrCodeLinks = txtLinkList.Select(link => GetQrCodeParam(link));
            return qrCodeLinks.ToDictionary(itm => itm.Language);
        }

        private QrCodeLinkModel GetQrCodeParam(KeyValuePair<string, string> link)
        {
            var icon = GetQrCodeIcon();
            var language = link.Key;
            var textLink = link.Value;
            var qrCode = _qrCodeGeneratorService.GenerateBase64QrCode(textLink, icon, true);
            return new QrCodeLinkModel
            {
                Language = language,
                Link = textLink,
                QrCode = qrCode
            };
        }

        private Image GetQrCodeIcon()
        {
            var qrIconFileName = AppSetting.GetValue("QrCodeIcon");
            var path = HttpContext.Server.MapPath($"~/www/build/styles/Images/{qrIconFileName}");
            var img = new Bitmap(path);
            return img;
        }
    }
}