using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using Fanex.Affiliate.Admin.Core.Utilities;
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

        public ActionResult Index(PaymentRequestParamFromHome param)
        {
            return View("~/Areas/Portal/Views/PaymentRequest/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPaymentRequestList(PaymentRequestParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _paymentRequestService.GetPaymentRequestList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPaymentRequestDetail(long transId)
        {
            if (transId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _paymentRequestService.GetPaymentRequestDetail(transId)
                };
            }

            return new JsonNetResult();
        }

        [FunctionAuthorize(FunctionName = Function.Payment, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdatePaymentRequest(PaymentRequestUpdateParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                var emailParam = new PaymentRequestUpdateEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this),
                };

                return new JsonNetResult
                {
                    Data = _paymentRequestService.UpdatePaymentRequest(param, emailParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Transaction not exists!"
                }
            };
        }
    }
}
