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

    [FunctionAuthorize(FunctionName = Function.MemberMenu)]
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
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Member/Index.cshtml", new MemberListViewModel());
        }

        [HttpPost]
        [FunctionAuthorize(FunctionName = Function.ActiveMembers)]
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
                Data = ResultInfo.GenerateErr("Deposit User Name(s) is required!")
            };
        }
    }
}
