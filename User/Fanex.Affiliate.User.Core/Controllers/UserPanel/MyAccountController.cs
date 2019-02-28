namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels;
    using Newtonsoft.Json;
    using Resources;
    using Extensions;

    [CustomAuthorize]
    public class MyAccountController : BaseController
    {
        private const string SimplifiedChineseCode = "zh-CN";
        private const string TraditionalChineseCode = "zh-TW";
        private readonly ISessionService _sessionService;
        private readonly IAccountRepository _accountRepository;
        public MyAccountController(ISessionService sessionService,
            ILanguageService languageService, IAccountRepository accountRepository)
            : base(sessionService, languageService)
        {
            _sessionService = sessionService;
            _accountRepository = accountRepository;
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var model = _accountRepository.GetAccountInfo(_sessionService.UserSession.AffId);
            model.IsRenderState = _sessionService.CurrentLanguage == SimplifiedChineseCode || _sessionService.CurrentLanguage == TraditionalChineseCode;
            if (!string.IsNullOrEmpty(model.ContactApps))
            {
                model.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(model.ContactApps).OrderBy(p => p.Order);
            }

            var preferredLanguage = model.PreferredLanguage;
            var languageName = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.Equals(preferredLanguage))?.EnglishName ?? preferredLanguage;
            model.LanguageName = SystemLanguage.GetLanguageText(languageName);

            return View("~/Views/UserPanel/MyAccount/index.cshtml", model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AccountInformation model)
        {
            if (_sessionService.IsInternal)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.ErrAuthorized });
            }

            if (!IsValidAppContactList(model.ContactAppList))
            {
                return Json(new ResultInfo { IsOk = false, Message = Error.RequireIMAccount });
            }

            var userSession = _sessionService.UserSession;
            var userAccount = _accountRepository.GetAccountInfo(userSession.AffId);
            model.PlayerUsername = !string.IsNullOrEmpty(userAccount?.PlayerUsername) ?
                userAccount.PlayerUsername :
                Server.HtmlEncode(model.PlayerUsername);

            var depositAccount = _accountRepository.GetDepositPlayerAccount(model.PlayerUsername);

            if (!depositAccount.IsValid)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.errDepositDBConnectFailed });
            }

            var accountUpdateParam = new UpdateAccountParam
            {
                AffId = userSession.AffId,
                Phone = Server.HtmlEncode(model.Phone),
                SiteUrl = Server.HtmlEncode(model.SiteUrl ?? string.Empty),
                MoreSiteUrls = Server.HtmlEncode(model.MoreSiteUrls ?? string.Empty),
                NoSiteDesc = Server.HtmlEncode(model.NoSiteDesc ?? string.Empty),
                Player_UserName = model.PlayerUsername,
                Ip = Request.UserHostAddress,
                Language = _sessionService.CurrentLanguage,
                ContactApps = JsonConvert.SerializeObject(model.ContactAppList.Select(p => new ContactApp
                {
                    Name = Server.HtmlEncode(p.Name),
                    Value = Server.HtmlEncode(p.Value),
                    Order = p.Order
                })),
            };

            var updatingResult = _accountRepository.UpdateAccountInformation(accountUpdateParam);

            return Json(updatingResult);
        }

        private static bool IsValidAppContactList(IEnumerable<ContactApp> contactAppList)
        {
            return contactAppList != null && !contactAppList.All(c => string.IsNullOrEmpty(c.Value));
        }
    }
}