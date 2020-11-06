using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.Payment)]
    public class PaymentRequestController : ReadOnlySessionBaseController
    {
        private readonly IPaymentRequestService _paymentRequestService;

        public PaymentRequestController(
            ISessionService sessionService,
            IPaymentRequestService paymentRequestService) : base(sessionService)
        {
            _paymentRequestService = paymentRequestService;
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
                var paymentRequestUpdateParam = new PaymentRequestUpdateParam
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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Transaction not exists!"
                }
            };
        }
    }
}
