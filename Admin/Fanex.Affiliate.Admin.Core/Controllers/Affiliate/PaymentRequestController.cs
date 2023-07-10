using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.PaymentRequest)]
    public class PaymentRequestController : ReadOnlySessionBaseController
    {
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IAdjustmentService _adjustmentService;

        public PaymentRequestController(
            ISessionService sessionService,
            IPaymentRequestService paymentRequestService,
            IAdjustmentService adjustmentService) : base(sessionService)
        {
            _paymentRequestService = paymentRequestService;
            _adjustmentService = adjustmentService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index(PaymentRequestParamFromHome param)
        {
            return View("~/Areas/Portal/Views/Affiliate/PaymentRequest.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
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
        [UpdateUnauthorizedProperty]
        public ActionResult GetPaymentRequestDetail(long transId)
        {
            if (transId >= Validation.MinId)
            {
                var paymentDetail = _paymentRequestService.GetPaymentRequestDetail(transId);
                var balanceBF = _adjustmentService.GetBalanceBroughtForward(paymentDetail.AffId);
                paymentDetail.BalanceBF = balanceBF;

                return new JsonNetResult
                {
                    Data = paymentDetail
                };
            }

            return new JsonNetResult();
        }

        [FunctionAuthorize(FunctionName = Function.PaymentRequest, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdatePaymentRequest(PaymentUpdateParam param)
        {
            if (ModelState.IsValid)
            {
                var paymentRequestUpdateParam = new PaymentUpdateParam
                {
                    UserId = SessionService.UserId,
                    Ip = Request.UserHostAddress,
                    TransId = param.TransId,
                    IsApproved = param.IsApproved,
                    AdminDesc = Server.HtmlEncode(param.AdminDesc),
                    AffDesc = Server.HtmlEncode(param.AffDesc)
                };

                var emailParam = new PaymentRequestUpdateEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this),
                };

                return new JsonNetResult
                {
                    Data = _paymentRequestService.UpdatePaymentRequest(paymentRequestUpdateParam, emailParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("Transaction not exists!")
            };
        }
    }
}
