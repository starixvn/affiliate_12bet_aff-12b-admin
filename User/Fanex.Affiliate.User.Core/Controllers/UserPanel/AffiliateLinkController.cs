namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Web.Mvc;
    using DTO.UserPanel.AccountOverview;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Marketing;

    public sealed class AffiliateLinkController : BaseMarketingController
    {
        private readonly IAppSetting _appSetting;
        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;

        public AffiliateLinkController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService systemService,
            IMarketingService marketingService,
            IQrCodeGeneratorService QrCodeGeneratorService
            ) : base(sessionService, systemService, marketingService)
        {
            _appSetting = appSetting;
            _qrCodeGeneratorService = QrCodeGeneratorService;
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult Index()
        {
            var linkForm = GetLinkFormViewModel();
            var selectedPage = linkForm.MemberSitePageList.FirstOrDefault()?.Value ?? string.Empty;

            var linkList = GetTextLinkList(linkForm.SelectedTargetCountry, linkForm.SelectedLanguage, selectedPage, linkForm.SelectedDomain, linkForm.LanguageList);
            var qrCodes = GetQrCodes(linkList);

            var viewModel = new AffiliateLinkViewModel
            {
                QrCodeList = qrCodes,
                LinkForm = linkForm
            };

            return View("~/Views/UserPanel/AccountOverview/AffiliateLink/Index.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GenerateLink(string selectedTargetCountry, string selectedLanguage, string selectedPage, int? selectedDomainId)
        {
            if (selectedDomainId == null || string.IsNullOrEmpty(selectedPage))
            {
                return View("~/Views/UserPanel/AccountOverview/Partial/_QrCodeLinkList.cshtml", new Dictionary<string, QrCodeLinkModel>());
            }

            var selectedDomain = GetDomainListByTargetCountry(selectedTargetCountry, SessionService.IsInternal)
                .FirstOrDefault(d => d.DomainId == selectedDomainId);

            var languageList = string.IsNullOrEmpty(selectedLanguage)
                ? SystemHelper.GetTargetLanguageList(SystemService.GetSystemTargetLanguagesByCountry(selectedTargetCountry), selectedLanguage)
                : null;

            var textLinkList = GetTextLinkList(selectedTargetCountry, selectedLanguage, selectedPage, selectedDomain, languageList);
            var qrCodeParamsDic = GetQrCodes(textLinkList);

            return View("~/Views/UserPanel/AccountOverview/Partial/_QrCodeLinkList.cshtml", qrCodeParamsDic);
        }

        private IDictionary<string, QrCodeLinkModel> GetQrCodes(IEnumerable<AffiliateLink> txtLinkList)
        {
            var qrCodeLinks = txtLinkList.Select(link => GetQrCodeParam(link));
            return qrCodeLinks.ToDictionary(itm => itm.Language);
        }

        private QrCodeLinkModel GetQrCodeParam(AffiliateLink link)
        {
            using (var icon = GetQrCodeIcon(link.LanguageCode))
            {
                var language = link.Language;
                var textLink = link.Link;
                var qrCode = _qrCodeGeneratorService.GenerateBase64QrCode(textLink, icon, true);
                return new QrCodeLinkModel
                {
                    Language = SystemHelper.GetLanguageText(language),
                    Link = textLink,
                    QrCode = qrCode
                };
            }
        }

        private Image GetQrCodeIcon(string selectedLanguage)
        {
            var qrIconFileName = string.Format(_appSetting.QrCodeIconPathTemplate, selectedLanguage);
            var path = HttpContext.Server.MapPath($"~/www/build/styles/Images/{qrIconFileName}");
            return _qrCodeGeneratorService.GetImage(path);
        }

        private IEnumerable<AffiliateLink> GetTextLinkList(string selectedTargetCountry, string selectedLanguage, string selectedPage,
            Domain selectedDomain, IEnumerable<SelectListItem> languageList)
        {
            if (selectedDomain == null)
            {
                return new List<AffiliateLink>();
            }

            var userSession = SessionService.UserSession;

            var linkGenerationParam = new LinkGenerationParam
            {
                AffId = userSession.AffId,
                TextLinkTemplate = _appSetting.TextLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedTargetCountry = selectedTargetCountry,
                SelectedLanguage = selectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                LanguageList = languageList
            };

            return MarketingService.GenerateLinkList(linkGenerationParam);
        }
    }
}