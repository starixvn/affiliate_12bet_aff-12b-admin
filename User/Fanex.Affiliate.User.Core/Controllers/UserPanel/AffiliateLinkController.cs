namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.UserPanel.Marketing;

    public class AffiliateLinkController : BaseMarketingController
    {
        public AffiliateLinkController(ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            IMarketingService marketingService) : base(sessionService,
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

            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.FirstOrDefault()?.PageCode ?? string.Empty;

            var textParam = new MarketingTextParam
            {
                AffId = userSession.AffId,
                Language = userSession.PreferredLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                ScriptUrl = AppSetting.MediaScriptUrl,
                TextLinkTemplate = AppSetting.TextLinkTemplate
            };

            var viewModel = new MarketingViewModel
            {
                SearchForm = searchForm,
                LinkForm = linkForm,
                MarketingDataList = GetTextList(textParam)
            };

            ViewBag.TotalItem = textParam.Nrows;

            return View("~/Views/UserPanel/AccountOverview/AffiliateLink/Index.cshtml", viewModel);
        }
    }
}