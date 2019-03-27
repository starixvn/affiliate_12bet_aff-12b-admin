using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Payment)]
    public class PaymentRequestController : BaseController
    {
        private readonly IPaymentRequestService _paymentRequestService;

        public PaymentRequestController(
            ISessionService sessionService,
            IPaymentRequestService paymentRequestService) : base(sessionService)
        {
            _paymentRequestService = paymentRequestService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/PaymentRequest/Index.cshtml");
        }

        [HttpPost]
        public ActionResult GetPaymentRequestList(PaymentRequestParam param)
        {
            return new JsonNetResult
            {
                Data = _paymentRequestService.GetPaymentRequestList(param)
            };
        }

        [HttpGet]
        public ActionResult GetPaymentRequestDetail(long transId)
        {
            var model = _paymentRequestService.GetPaymentRequestDetail(transId);

            return View("~/Areas/Portal/Views/PaymentRequest/GetPaymentRequestDetail.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPaymentRequestInfo(long transId)
        {
            return new JsonNetResult
            {
                Data = _paymentRequestService.GetPaymentRequestDetail(transId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult UpdatePaymentRequest(PaymentRequestUpdateParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;

            return new JsonNetResult
            {
                Data = _paymentRequestService.UpdatePaymentRequest(param)
            };
        }
    }
}
