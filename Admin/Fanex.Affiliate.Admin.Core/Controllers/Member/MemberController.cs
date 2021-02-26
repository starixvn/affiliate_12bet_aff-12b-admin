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
    public class MemberController : ReadOnlySessionBaseController
    {
        private readonly IMemberService _memberService;
        public MemberController(
            ISessionService sessionService,
            IMemberService memberService) : base(sessionService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public ActionResult Index(string affName)
        {
            var model = new MemberListViewModel
            {
                AffName = affName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MemberDetail)]
        public ActionResult GetMemberList(MemberParam param)
        {
            return new JsonNetResult
            {
                Data = _memberService.GetMemberList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MemberDetail, IsHost = true)]
        public ActionResult UpdateMemberStatus(UpdateMemberStatusParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _memberService.UpdateMemberStatus(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "No member specificed!"
                }
            };
        }
    }
}
