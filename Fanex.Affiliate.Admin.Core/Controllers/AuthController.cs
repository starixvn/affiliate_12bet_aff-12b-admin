using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CheckUMFilter]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly ISessionService _sessionService;
        public AuthController(IAuthRepository authRepository, ISessionService sessionService)
        {
            _authRepository = authRepository;
            _sessionService = sessionService;
        }
        [HttpGet]
        public ActionResult RecheckIn()
        {
            return View("~/Areas/Portal/Views/Auth/RecheckIn.cshtml");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Ping(long time)
        {
            var isKickedOut = true;
            var isAuthorized = HttpContext.Request.IsAuthenticated && HttpContext.User.Identity.IsAuthenticated;
            if (isAuthorized && _sessionService.OnUserId > 0 && _sessionService.UserId > 0)
            {
                isKickedOut = _authRepository.ReCheckin(_sessionService.OnUserId);
            }
            return Json(new { IsKickedOut = isKickedOut, Message = isKickedOut ? Lang.MsgKickout : string.Empty }, JsonRequestBehavior.AllowGet);
        }
    }
}
