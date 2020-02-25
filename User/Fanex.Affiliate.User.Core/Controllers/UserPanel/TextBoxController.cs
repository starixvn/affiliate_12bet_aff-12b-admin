namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO.UserPanel;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using ViewModels.UserPanel.Marketing;

    [CheckUMFilter]
    [CustomAuthorize]
    public class TextBoxController : OldBaseMarketingController
    {
        public TextBoxController(ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            IOldMarketingService marketingService) : base(sessionService,
                appSetting,
                languageService,
                marketingService)
        {
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var userSession = SessionService.UserSession;

            var searchForm = GetSearchFormViewModel(false);
            var linkForm = GetLinkFormViewModel();

            var selectedLanguage = userSession.PreferredLanguage;
            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.FirstOrDefault()?.PageCode ?? string.Empty;

            var textParam = new OldMarketingTextParam
            {
                AffId = userSession.AffId,
                Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(selectedLanguage))?.EnglishName,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                ScriptUrl = _appSetting.MediaScriptUrl,
                TextLinkTemplate = _appSetting.TextLinkTemplate
            };

            var viewModel = new OldMarketingViewModel
            {
                SearchForm = searchForm,
                LinkForm = linkForm,
                TextLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, searchForm.LanguageList),
                MarketingDataList = GetTextList(textParam)
            };

            ViewBag.TotalItem = textParam.Nrows;

            return View("~/Views/UserPanel/Marketing/TextBox/Index.cshtml", viewModel);
        }

        private IEnumerable<MarketingItem> GetTextList(OldMarketingTextParam textParam)
        {
            if (textParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return _oldMarketingService.GetTextList(textParam);
        }
    }
}