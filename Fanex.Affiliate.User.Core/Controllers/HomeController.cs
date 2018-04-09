using System;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly ISessionService _sessionService;

        public HomeController(IAuthRepository authRepository, ISessionService sessionService)
        {
            _authRepository = authRepository;
            _sessionService = sessionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CaptchaImage()
        {
            var captcha = new Random().Next(1000, 9999).ToString();
            _sessionService.CaptchaCode = captcha;

            return File(CaptchaGenerator.GenerateImageBuffer(captcha, 65, 26), "image/Jpeg");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            var result = new JResult
            {
                IsOk = false,
                Message = string.Empty
            };

            if (string.IsNullOrEmpty(user.UserName))
            {
                result.Message = "UserName is empty";
                return Json(result);
            }
            else if (string.IsNullOrEmpty(user.EncryptedPass))
            {
                result.Message = "Password is empty";
                return Json(result);
            }
            else if (!user.ValidationKey.Equals(_sessionService.CaptchaCode))
            {
                result.Message = "Validation Code is not equal";
                return Json(result);
            }

            result.IsOk = true;
            return Json(result);
        }
    }
}