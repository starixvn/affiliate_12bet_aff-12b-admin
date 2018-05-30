namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using Constants;
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
        private readonly ICookieService _cookieService;
        private readonly ICryptographyService _cryptographyService;

        public PasswordController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            ICookieService cookieService,
            ICryptographyService cryptographyService)
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
            _cookieService = cookieService;
            _cryptographyService = cryptographyService;
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
            var keyString = Generator.GetRandomString(32);
            var ivString = Generator.GetRandomString(16);

            ViewBag.KeyString = keyString;
            ViewBag.IVString = ivString;

            _cookieService.SetCookie(CookieName.EncryptionKey, keyString);
            _cookieService.SetCookie(CookieName.InitializationVector, ivString);

            return View();
        }

        [HttpPost]
        [Authenticate]
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
                param.OldPassword = _cryptographyService.EncryptPassword(param.OldPassword);
                param.NewPassword = _cryptographyService.EncryptPassword(param.NewPassword);

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