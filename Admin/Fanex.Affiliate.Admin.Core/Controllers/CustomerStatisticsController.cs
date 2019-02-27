using System;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

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

        public ActionResult Index(AffiliateLinkParam affiliateLinkParam)
        {
            return View(affiliateLinkParam);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCustomerStatistics(CustomerStatisticsParam param)
        {
            if (!param.MonthDate.HasValue)
            {
                param.MonthDate = DateTime.Now;
            }

            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            return Json(_customerRepository.GetCustomerStatistics(param));
        }
    }
}