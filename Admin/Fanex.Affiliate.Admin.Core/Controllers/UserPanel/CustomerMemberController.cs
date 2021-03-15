using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.MemberDetail)]
    public class CustomerMemberController : ReadOnlySessionBaseController
    {
        private readonly ICustomerMemberService _customerMemberService;

        public CustomerMemberController(ISessionService sessionService, ICustomerMemberService customerMemberService) : base(sessionService)
        {
            _customerMemberService = customerMemberService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [ResetDefaultUnauthorizedProperty]
        public ActionResult GetCustomerMember(CustomerMemberParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _customerMemberService.GetCustomerMemberList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
