using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.Member
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MemberController : BaseController
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
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Member, Url = Url.Content("~/Portal/Member"), IsActive = true },
                new SubMenu { Name = Lang.ActiveMember, Url = Url.Content("~/Portal/ActiveMember") }
            };

            var model = new MemberListViewModel
            {
                SubMenuList = subMenu,
                AffName = affName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMemberList(MemberParam param)
        {
            return new JsonNetResult
            {
                Data = _memberService.GetMemberList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        public ActionResult DeleteMember(DeleteMemberParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _memberService.DeleteMember(param)
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
