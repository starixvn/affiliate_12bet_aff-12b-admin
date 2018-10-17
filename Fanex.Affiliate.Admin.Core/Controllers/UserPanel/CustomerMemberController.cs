using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class CustomerMemberController : BaseController
    {
        private readonly ICustomerMemberRepository _customerMemberRepository;

        public CustomerMemberController(ISessionService sessionService, ICustomerMemberRepository customerMemberRepository) : base(sessionService)
        {
            _customerMemberRepository = customerMemberRepository;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/UserPanel/Member/Index.cshtml");
        }

        public ActionResult GetCustomerMemberList(CustomerMemberParam param)
        {
            return Json(_customerMemberRepository.GetCustomerMemberList(param));
        }
    }
}
