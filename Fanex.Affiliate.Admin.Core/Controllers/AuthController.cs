using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Repository;
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
        [CustomAuthorize]
        public ActionResult RecheckIn()
        {
            return View("~/Areas/Portal/View/Auth/RecheckIn.cshtml");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        [CustomAuthorize]
        public ActionResult RecheckIn(DateTime time)
        {
            var isKickedOut = true;
            if (_sessionService.OnUserId > 0 && _sessionService.UserId > 0)
            {
                isKickedOut = _authRepository.ReCheckin(_sessionService.OnUserId);
            }
            return Json(new { IsKickedOut = isKickedOut }, JsonRequestBehavior.AllowGet);
        }
    }
}
