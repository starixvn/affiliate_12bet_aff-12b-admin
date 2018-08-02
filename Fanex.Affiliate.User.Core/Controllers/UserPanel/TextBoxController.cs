namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using ViewModels.UserPanel.Marketing;

    [CheckUMFilter]
    [CustomAuthorize]
    public class TextBoxController : BaseMarketingController
    {

        public TextBoxController(ISessionService sessionService, IAppSetting appSetting, IMarketingService marketingService,
            IAccountRepository accountRepository) : base(sessionService, appSetting, marketingService,accountRepository)
        {
           
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var userSession = _sessionService.UserSession;

            var searchForm = GetSearchFormViewModel(false);
            var linkForm = GetLinkFormViewModel();

            var selectedLanguage = userSession.PreferredLanguage;
            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.First().Value;

            var textParam = new MarketingTextParam
            {
                AffId = userSession.AffId,
                Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(selectedLanguage))?.EnglishName,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                ScriptUrl = _appSetting.MediaScriptUrl,
                TextLinkTemplate = _appSetting.TextLinkTemplate
            };

            var viewModel = new MarketingViewModel
            {
                SearchForm = searchForm,
                LinkForm = linkForm,
                TextLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, searchForm.LanguageList),
                MarketingDataList = GetTextList(textParam)
            };

            ViewBag.TotalItem = textParam.Nrows;

            return View("~/Views/UserPanel/Marketing/TextBox/index.cshtml", viewModel);
        }


        private IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            if (textParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return _marketingService.GetTextList(textParam);
        }

       


       

    }
}