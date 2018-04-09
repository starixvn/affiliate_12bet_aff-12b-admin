using System;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;
        public HomeController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CaptchaImage()
        {
            var captcha = new Random().Next(1000, 9999);
            return File(CaptchaGenerator.GenerateImageBuffer(captcha.ToString(), 65, 26), "image/Jpeg");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            var result = new JResult
            {

            };

            return Json(result);
        }
    }
}
