using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Payment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Payment)]
    public class PaymentRequestController : BaseController
    {
        private readonly IPaymentRequestRepository _paymentRequestRepository;

        public PaymentRequestController(
            ISessionService sessionService,
            IPaymentRequestRepository paymentRequestRepository) : base(sessionService)
        {
            _paymentRequestRepository = paymentRequestRepository;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/PaymentRequest/Index.cshtml");
        }

        [HttpPost]
        public ActionResult GetPaymentRequestList(PaymentRequestParam param)
        {
            var model = _paymentRequestRepository.GetPaymentRequestList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [HttpGet]
        public ActionResult GetPaymentRequestDetail(long transId)
        {
            var model = _paymentRequestRepository.GetPaymentRequestDetail(transId);

            return View("~/Areas/Portal/Views/PaymentRequest/GetPaymentRequestDetail.cshtml", model);
        }

        [HttpPost]
        public ActionResult GetPaymentRequestInfo(long transId)
        {
            var model = _paymentRequestRepository.GetPaymentRequestDetail(transId);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
