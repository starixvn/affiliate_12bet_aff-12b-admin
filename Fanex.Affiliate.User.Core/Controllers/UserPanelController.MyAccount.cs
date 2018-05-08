namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public partial class UserPanelController
    {
        [HttpGet]
        public ActionResult MyAccount()
        {
            var model = _accountRepository.GetAccountInfo(SessionService.UserSession.AffId);
            model.IsRenderState = SessionService.CurrentLanguage == "zh-CN" || SessionService.CurrentLanguage == "zh-TW";

            return View(model);
        }
    }
}