namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using Filters;
    using MetalModels;
    using Resources;
    using ViewModels;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult MyAccount()
        {
            var model = _accountRepository.GetAccountInfo(SessionService.UserSession.AffId);
            model.IsRenderState = SessionService.CurrentLanguage == "zh-CN" || SessionService.CurrentLanguage == "zh-TW";

            var preferredLanguage = model.PreferredLanguage;
            model.LanguageName = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.Equals(preferredLanguage))?.EnglishName ?? preferredLanguage;

            return View(model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult MyAccount(AccountInformationViewModel model)
        {
            if (SessionService.IsInternal)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.ErrAuthorized });
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
                MobilePhone = Server.HtmlEncode(model.MobilePhone),
                SiteUrl = Server.HtmlEncode(model.SiteUrl ?? string.Empty),
                MoreSiteUrls = Server.HtmlEncode(model.MoreSiteUrls ?? string.Empty),
                NoSiteDesc = Server.HtmlEncode(model.NoSiteDesc ?? string.Empty),
                Player_UserName = model.PlayerUsername,
                Ip = Request.UserHostAddress,
                Language = SessionService.CurrentLanguage
            };

            var updatingResult = _accountRepository.UpdateAccountInformation(accountUpdateParam);

            return Json(updatingResult);
        }
    }
}