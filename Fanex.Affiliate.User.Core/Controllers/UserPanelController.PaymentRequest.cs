namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using Extensions;
    using Filters;
    using MetalModels.Email;
    using MetalModels.UserPanel;
    using Resources;
    using Utilities;
    using ViewModels.UserPanel;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult PaymentRequest()
        {
            return View(GetViewModel());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult PaymentRequestPartial()
        {
            return PartialView("~/Views/UserPanel/PaymentPartial/_Main.cshtml", GetViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            var resultInfo = new ResultInfo { IsOk = true };

            if (SessionService.IsInternal)
            {
                resultInfo.IsOk = false;
                resultInfo.Message = Lang.ErrAuthorized;
            }

            if (resultInfo.IsOk)
            {
                paymentCreationParam.AffId = SessionService.UserSession.AffId;
                paymentCreationParam.Language = SessionService.CurrentLanguage;
                paymentCreationParam.Ip = Request.UserHostAddress;
                paymentCreationParam.IsInternal = SessionService.IsInternal;
                paymentCreationParam.IsLocalTest = AppSetting.IsLocal;

                var emailParam = new PaymentEmailParam
                {
                    MailCc = AppSetting.MailCc,
                    MailContact = AppSetting.MailContact,
                    MailBcc = AppSetting.MailBcc,
                    RootDomain = SiteDomainHelper.GetRootUrl(this)
                };

                resultInfo = _paymentService.CreatePaymentRequest(paymentCreationParam, emailParam);
            }

            return Json(resultInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelPaymentRequest(PaymentCancelParam cancelParam)
        {
            var resultInfo = new ResultInfo { IsOk = false };

            if (SessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }
            else
            {
                cancelParam.UserId = SessionService.UserSession.AffId;
                cancelParam.Language = SessionService.CurrentLanguage;
                cancelParam.Ip = Request.UserHostAddress;

                var emailParam = new PaymentEmailParam
                {
                    MailCc = AppSetting.MailCc,
                    MailContact = AppSetting.MailContact,
                    MailBcc = AppSetting.MailBcc,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    Amount = cancelParam.Amount,
                    RequestDate = cancelParam.RequestDate,
                    TransId = cancelParam.TransId
                };

                resultInfo = _paymentService.CancelPaymentRequest(cancelParam, emailParam);
            }

            return Json(resultInfo);
        }

        private PaymentViewModel GetViewModel()
        {
            var paymentParam = new PaymentParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal
            };
            var paymentList = _paymentService.GetPaymentList(paymentParam);

            var viewModel = new PaymentViewModel
            {
                PaymentList = paymentList,
                BalBroughtForward = paymentParam.BalBroughtForward,
                MinAmount = paymentParam.MinAmount,
                PreferredPayment = paymentParam.PreferredPayment,
                PendingPayment = paymentList.FirstOrDefault(p => p.Status.MatchWith(PaymentStatus.Pending)),
                IsValidDate = _paymentService.IsInDateRange() || AppSetting.IsLocal
            };

            return viewModel;
        }
    }
}