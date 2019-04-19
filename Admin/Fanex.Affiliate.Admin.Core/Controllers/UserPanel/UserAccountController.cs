using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class UserAccountController : BaseController
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountController(
            ISessionService sessionService,
            IUserAccountRepository userAccountRepository) : base(sessionService)
        {
            _userAccountRepository = userAccountRepository;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            return View("~/Areas/Portal/Views/UserPanel/UserAccount/Index.cshtml", param);
        }

        [HttpPost]
        public JsonResult GetUserAccountInfo (int affId)
        {
            return new JsonResult
            {
                Data = _userAccountRepository.GetUserAccountInfo(affId == 0 ? SessionService.AffId : affId)
            };
        }

    }
}
