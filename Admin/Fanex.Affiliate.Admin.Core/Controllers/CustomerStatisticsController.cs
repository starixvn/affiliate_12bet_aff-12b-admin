using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerStatisticsController : BaseController
    {
        private readonly ICustomerStatisticsRepository _customerRepository;

        public CustomerStatisticsController(ISessionService sessionService, ICustomerStatisticsRepository customerRepository) : base(sessionService)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCustomerStatistics(CustomerStatisticsParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _customerRepository.GetCustomerStatistics(param)
                };
            }

            return new JsonNetResult();
        }
    }
}