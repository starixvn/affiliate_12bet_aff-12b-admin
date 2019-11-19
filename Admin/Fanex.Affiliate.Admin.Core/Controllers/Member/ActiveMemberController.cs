using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.Member
{
    [FunctionAuthorize(FunctionName = Function.ActiveMembers)]
    public class ActiveMemberController: BaseController
    {
        private readonly IActiveMemberService _activeMemberService;

        public ActiveMemberController(
            ISessionService sessionService,
            IActiveMemberService activeMemberService) : base(sessionService)
        {
            _activeMemberService = activeMemberService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.Member, Url = Url.Content("~/Portal/Member") },
                new SubMenu { Name = Lang.ActiveMember, Url = Url.Content("~/Portal/ActiveMember"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/Member/ActiveMember.cshtml", subMenu);
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
