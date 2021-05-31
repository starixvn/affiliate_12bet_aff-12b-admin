using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.ViewModels.Member;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Member
{
    [FunctionAuthorize(FunctionName = Function.MemberMenu)]
    public class RegisteredController : ReadOnlySessionBaseController
    {
        private readonly IRegisteredMemberService _registeredMemberService;

        public RegisteredController(
            ISessionService sessionService,
            IRegisteredMemberService registeredMemberService) : base(sessionService)
        {
            _registeredMemberService = registeredMemberService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Member/Index.cshtml", new MemberListViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MemberDetail)]
        public ActionResult GetRegisteredMemberList(MemberParam param)
        {
            return new JsonNetResult
            {
                Data = _registeredMemberService.GetRegisteredMemberList(param)
            };
        }
    }
}
