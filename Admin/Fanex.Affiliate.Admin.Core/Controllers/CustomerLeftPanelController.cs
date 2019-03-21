using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerLeftPanelController : BaseController
    {
        private readonly ICustomerLeftPanelRepository _customerLeftPanelRepository;

        public CustomerLeftPanelController(ISessionService sessionService, ICustomerLeftPanelRepository customerLeftPanelRepository) : base(sessionService)
        {
            _customerLeftPanelRepository = customerLeftPanelRepository;
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        [Compress]
        public ActionResult GetAffiliateInfo(string affUsername)
        {
            return new JsonNetResult
            {
                Data = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUsername)
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        [Compress]
        public ActionResult GetOnlineList(string affUsername)
        {
            return new JsonNetResult
            {
                Data = _customerLeftPanelRepository.GetOnlineList(affUsername)
            };
        }
    }
}
