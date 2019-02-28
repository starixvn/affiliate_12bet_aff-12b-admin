using System;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerMemberController : BaseController
    {
        private readonly ICustomerMemberService _customerMemberService;

        public CustomerMemberController(ISessionService sessionService, ICustomerMemberService customerMemberService) : base(sessionService)
        {
            _customerMemberService = customerMemberService;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam affiliateLinkParam)
        {
            var viewModel = new CustomerMemberViewModel
            {
                CanViewMemberEmail = SessionService.UserPermissionList.Any(i =>
                                        i.FunctionName.Equals(Function.MemberEmail, StringComparison.OrdinalIgnoreCase)),
                AffiliateLinkParam = affiliateLinkParam
            };

            return View("~/Areas/Portal/Views/UserPanel/CustomerMember/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCustomerMember(CustomerMemberParam param)
        {
            return Json(_customerMemberService.GetCustomerMember(param));
        }
    }
}
