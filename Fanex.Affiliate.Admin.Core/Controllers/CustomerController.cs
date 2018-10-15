using System;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ISessionService sessionService, ICustomerRepository customerRepository) : base(sessionService)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomerStatistics(CustomerStatisticsParam param)
        {
            if (!param.MonthDate.HasValue)
            {
                param.MonthDate = DateTime.Now;
            }

            return Json(_customerRepository.GetCustomerStatistics(param));
        }
    }
}