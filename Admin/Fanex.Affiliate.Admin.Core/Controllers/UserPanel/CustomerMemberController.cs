using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;

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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCustomerMember(CustomerMemberParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _customerMemberService.GetCustomerMember(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
