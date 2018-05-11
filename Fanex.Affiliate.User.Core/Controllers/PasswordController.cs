namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using CoreService;
    using DTO;
    using MetalModels;
    using Repository.UserPanel;
    using Resources;
    using Utilities;

    public class PasswordController : BaseController
    {
        private readonly IAccountRepository _accountRepository;

        public PasswordController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository)
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(UpdatePasswordParam param)
        {
            if (SessionService.IsInternal)
            {
                return Json(new ResultInfo { IsOk = false, Message = Lang.ErrAuthorized });
            }

            const string MessageKey = "ResultMessage";

            if (ModelState.IsValid)
            {
                param.AffId = SessionService.UserSession.AffId;
                param.Ip = RequestHelper.GetClientIPAddress();
                param.Language = SessionService.CurrentLanguage;

                var result = _accountRepository.UpdatePassword(param);

                if (!result.IsOk)
                {
                    TempData[MessageKey] = result.Message;
                }
            }
            else
            {
                TempData[MessageKey] = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            return RedirectToAction("Update");
        }
    }
}