namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Filters;
    using MetalModels;
    using Newtonsoft.Json;
    using Resources;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult MyAccount()
        {
            var model = _accountRepository.GetAccountInfo(SessionService.UserSession.AffId);
            model.IsRenderState = SessionService.CurrentLanguage == "zh-CN" || SessionService.CurrentLanguage == "zh-TW";
            if (!string.IsNullOrEmpty(model.ContactApps))
            {
                model.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(model.ContactApps).OrderBy(p => p.Order);
            }

            var preferredLanguage = model.PreferredLanguage;
            model.LanguageName = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.Equals(preferredLanguage))?.EnglishName ?? preferredLanguage;

            return View(model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult MyAccount(AccountInformation model)
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
                Language = SessionService.CurrentLanguage,
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
            return contactAppList != null && contactAppList.All(c => !string.IsNullOrEmpty(c.Value));
        }
    }
}