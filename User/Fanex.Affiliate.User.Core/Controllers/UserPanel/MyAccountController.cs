namespace Fanex.Affiliate.User.Core.Controllers
{
    using BusinessService;
    using Constants;
    using CoreService;
    using DTO;
    using DTO.UserPanel;
    using Extensions;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using Filters;
    using MetalModels;
    using Newtonsoft.Json;
    using Resources;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Utilities;

    [CustomAuthorize]
    public class MyAccountController : BaseController
    {
        private const string SimplifiedChineseCode = "zh-CN";
        private const string TraditionalChineseCode = "zh-TW";

        private readonly IAccountService _accountService;

        public MyAccountController(
            ISessionService sessionService,
            ISystemService systemsService,
            IAccountService accountService) : base(sessionService, systemsService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var accountInfo = _accountService.GetAccountInfo(SessionService.UserSession.AffId);
            var newAccountInfo = new AccountInformation
            {
                AffId = accountInfo.AffId,
                UserName = accountInfo.UserName,
                AffCode = accountInfo.AffCode,
                Gender = accountInfo.Gender,
                Email = Server.HtmlDecode(accountInfo.Email),
                Phone = Server.HtmlDecode(accountInfo.Phone),
                ContactApps = Server.HtmlDecode(accountInfo.ContactApps),
                Address = Server.HtmlDecode(accountInfo.Address),
                City = Server.HtmlDecode(accountInfo.City),
                State = Server.HtmlDecode(accountInfo.State),
                Country = Server.HtmlDecode(accountInfo.Country),
                SiteUrl = Server.HtmlDecode(accountInfo.SiteUrl),
                MoreSiteUrls = Server.HtmlDecode(accountInfo.MoreSiteUrls),
                PreferredPayment = Server.HtmlDecode(accountInfo.PreferredPayment),
                PlayerUsername = Server.HtmlDecode(accountInfo.PlayerUsername),
                Postal = Server.HtmlDecode(accountInfo.Postal),
                PreferredLanguage = accountInfo.PreferredLanguage,
                NoSiteDesc = Server.HtmlDecode(accountInfo.NoSiteDesc),
                IsRenderState = SessionService.CurrentLanguage == SimplifiedChineseCode || SessionService.CurrentLanguage == TraditionalChineseCode,
                HasWebSite = accountInfo.HasWebSite,
                AffCurrency = accountInfo.AffCurrency,
                DefaultBaseCurrency = accountInfo.DefaultBaseCurrency
            };

            if (!string.IsNullOrEmpty(newAccountInfo.ContactApps))
            {
                newAccountInfo.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(newAccountInfo.ContactApps).OrderBy(p => p.Order);
            }

            var preferredLanguage = newAccountInfo.PreferredLanguage;
            var languageName = SystemService.GetSupportedLanguages().FirstOrDefault(l => l.Code.Equals(preferredLanguage))?.EnglishName ?? preferredLanguage;
            newAccountInfo.LanguageName = SystemLanguage.GetLanguageText(languageName);

            var baseCurrencyList = SystemService.GetBaseCurrencyList();

            var model = new MyAccountViewModel
            {
                IsInternal = SessionService.IsInternal,
                AccountInformation = newAccountInfo,
                BaseCurrencyList = CurrencyHelper.GetCurrencyList(baseCurrencyList, newAccountInfo.DefaultBaseCurrency)
            };

            return View("~/Views/UserPanel/MyAccount/index.cshtml", model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AccountInformation model)
        {
            if (SessionService.IsInternal)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.ErrAuthorized });
            }

            if (!IsValidAppContactList(model.ContactAppList))
            {
                return Json(new ResultInfo { IsOk = false, Message = Error.RequireIMAccount });
            }

            var userSession = SessionService.UserSession;
            var userAccount = _accountService.GetAccountInfo(userSession.AffId);
            model.PlayerUsername = !string.IsNullOrEmpty(userAccount?.PlayerUsername)
                ? userAccount.PlayerUsername
                : Server.HtmlEncode(model.PlayerUsername);

            var accountUpdateParam = new UpdateAccountParam
            {
                AffId = userSession.AffId,
                Phone = Server.HtmlEncode(model.Phone),
                SiteUrl = Server.HtmlEncode(model.SiteUrl ?? string.Empty),
                MoreSiteUrls = Server.HtmlEncode(model.MoreSiteUrls ?? string.Empty),
                NoSiteDesc = Server.HtmlEncode(model.NoSiteDesc ?? string.Empty),
                Ip = Request.GetClientIP(),
                Language = SessionService.CurrentLanguage,
                DefaultBaseCurrency = model.DefaultBaseCurrency,
                ContactApps = JsonConvert.SerializeObject(model.ContactAppList.Select(p => new ContactApp
                {
                    Name = Server.HtmlEncode(p.Name),
                    Value = Server.HtmlEncode(p.Value),
                    Order = p.Order
                })),
            };

            var updatingResult = _accountService.UpdateAccountInformation(accountUpdateParam);
            if (updatingResult.IsOk)
            {
                userSession.BaseCurrency = accountUpdateParam.DefaultBaseCurrency;
            }

            return Json(updatingResult);
        }

        private static bool IsValidAppContactList(IEnumerable<ContactApp> contactAppList)
        {
            return contactAppList != null && !contactAppList.All(c => string.IsNullOrEmpty(c.Value));
        }
    }
}