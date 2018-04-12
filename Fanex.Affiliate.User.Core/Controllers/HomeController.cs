using System;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISessionService sessionService, IAppSetting appSetting)
            : base(sessionService, appSetting)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CaptchaImage()
        {
            var captcha = new Random().Next(1000, 9999).ToString();
            SessionService.CaptchaCode = captcha;

            return File(CaptchaHelper.GenerateImageBuffer(captcha, 65, 26), "image/Jpeg");
        }
    }
}