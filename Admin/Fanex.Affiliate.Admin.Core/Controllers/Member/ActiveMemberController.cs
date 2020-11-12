using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.ViewModels.Member;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Member
{
    [FunctionAuthorize(FunctionName = Function.AffMember)]
    [FunctionAuthorize(FunctionName = Function.ActiveMembers)]
    public class ActiveMemberController : ReadOnlySessionBaseController
    {
        private readonly IActiveMemberService _activeMemberService;

        public ActiveMemberController(
            ISessionService sessionService,
            IActiveMemberService activeMemberService) : base(sessionService)
        {
            _activeMemberService = activeMemberService;
        }


        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index(string affName)
        {
            var model = new MemberListViewModel
            {
                AffName = affName
            };

            return View("~/Areas/Portal/Views/Member/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAllMembers(string userNames)
        {
            return new JsonNetResult
            {
                Data = _activeMemberService.GetAllMembers(userNames)
            };
        }

        [FunctionAuthorize(FunctionName = Function.ActiveMembers, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ActiveMember(ActiveMemberParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _activeMemberService.ActiveMember(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Deposit User Name(s) is required!"
                }
            };
        }
    }
}
