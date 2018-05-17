namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using CoreService;
    using DTO;
    using Filters;
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
        [Authenticate]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(UpdatePasswordParam param)
        {
            const string ResultKey = "UpdatePasswordResult";
            var resultInfo = new ResultInfo { IsOk = false };

            if (SessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }
            else if (ModelState.IsValid)
            {
                param.AffId = SessionService.UserSession.AffId;
                param.Ip = RequestHelper.GetClientIPAddress();
                param.Language = SessionService.CurrentLanguage;
                param.OldPassword = CryptographyHelper.Encrypt(param.OldPassword);
                param.NewPassword = CryptographyHelper.Encrypt(param.NewPassword);

                var updateResult = _accountRepository.UpdatePassword(param);
                resultInfo.IsOk = updateResult.IsOk;
                resultInfo.Message = updateResult.IsOk ? Lang.Passwordchanged : updateResult.Message;
            }
            else
            {
                resultInfo.Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            TempData[ResultKey] = resultInfo;

            return RedirectToAction("Update");
        }
    }
}