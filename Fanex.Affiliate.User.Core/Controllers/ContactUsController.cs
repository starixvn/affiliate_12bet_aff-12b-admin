namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using CoreService;
    using Utilities;

    public class ContactUsController : BaseController
    {
        public ContactUsController(
            ISessionService sessionService,
            IAppSetting appSetting)
            : base(sessionService, appSetting)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CaptchaImage()
        {
            var captcha = new Random().Next(1000, 9999).ToString();

            return File(CaptchaHelper.GenerateImageBuffer(captcha, 65, 26), "image/Jpeg");
        }
    }
}