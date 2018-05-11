namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using DTO;
    using MetalModels;
    using Resources;
    using Utilities;
    using ViewModels;

    public partial class UserPanelController
    {
        [HttpGet]
        public ActionResult MyAccount()
        {
            var model = _accountRepository.GetAccountInfo(SessionService.UserSession.AffId);
            model.IsRenderState = SessionService.CurrentLanguage == "zh-CN" || SessionService.CurrentLanguage == "zh-TW";

            return View(model);
        }

        [HttpPost]
        public ActionResult MyAccount(AccountInformationViewModel model)
        {
            if (SessionService.IsInternal)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.ErrAuthorized });
            }

            var userSession = SessionService.UserSession;
            var userAccount = _accountRepository.GetAccountInfo(userSession.AffId);
            if (!string.IsNullOrEmpty(userAccount.PlayerUsername))
            {
                model.PlayerUsername = userAccount.PlayerUsername;
            }
            else
            {
                model.PlayerUsername = Server.HtmlEncode(model.PlayerUsername);
            }

            var isValidDepositPlayer = _accountRepository.IsValidDepositPlayer(model.PlayerUsername);

            if (!isValidDepositPlayer)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.errDepositDBConnectFailed });
            }

            var accountUpdateParam = new AccountUpdateParam
            {
                AffId = userSession.AffId,
                Phone = Server.HtmlEncode(model.Phone),
                MobilePhone = Server.HtmlEncode(model.MobilePhone),
                SiteUrl = Server.HtmlEncode(model.SiteUrl),
                MoreSiteUrls = Server.HtmlEncode(model.MoreSiteUrls),
                NoSiteDesc = Server.HtmlEncode(model.NoSiteDesc),
                Player_UserName = model.PlayerUsername,
                Ip = RequestHelper.GetClientIPAddress(),
                Language = SessionService.CurrentLanguage
            };

            var updatingResult = _accountRepository.UpdateAccountInformation(accountUpdateParam);

            return Json(updatingResult);
        }
    }
}