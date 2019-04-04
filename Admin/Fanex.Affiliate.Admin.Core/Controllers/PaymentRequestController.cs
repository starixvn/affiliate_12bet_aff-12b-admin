using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Resources;
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

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/PaymentRequest/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPaymentRequestList(PaymentRequestParam param)
        {
            return new JsonNetResult
            {
                Data = _paymentRequestService.GetPaymentRequestList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPaymentRequestDetail(long transId)
        {
            return new JsonNetResult
            {
                Data = _paymentRequestService.GetPaymentRequestDetail(transId)
            };
        }

        [FunctionAuthorize(FunctionName = Function.Payment, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdatePaymentRequest(PaymentRequestUpdateParam param)
        {
            var isApproved = string.Equals(Lang.ApprovedLowerCase, param.Status, System.StringComparison.InvariantCultureIgnoreCase);
            var isRejected = string.Equals(Lang.RejectedLowerCase, param.Status, System.StringComparison.InvariantCultureIgnoreCase);
            var paymentRequestDetail = _paymentRequestService.GetPaymentRequestDetail(param.TransId);
            
            if ((isApproved || isRejected) && paymentRequestDetail != null &&
                string.Equals(Lang.PendingLowerCase, paymentRequestDetail.Status, System.StringComparison.InvariantCultureIgnoreCase))
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;
                param.AffId = paymentRequestDetail.AffId;
                param.Amount = paymentRequestDetail.Amount;
                param.TransDate = paymentRequestDetail.TransDate;

                var emailParam = new PaymentRequestUpdateEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this),
                    IsApproved = isApproved
                };

                return new JsonNetResult
                {
                    Data = _paymentRequestService.UpdatePaymentRequest(param, emailParam)
                };
            }

            return new JsonNetResult
            {
                Data = null
            };
        }
    }
}
