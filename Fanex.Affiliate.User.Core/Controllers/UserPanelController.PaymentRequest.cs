namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using Enum;
    using Extensions;
    using Filters;
    using MetalModels.Email;
    using MetalModels.UserPanel;
    using Resources;
    using Utilities;
    using ViewModels.UserPanel;

    public partial class UserPanelController
    {
        private const int BeginDay = 8;
        private const int EndDay = 15;

        [HttpGet]
        [AjaxOnly]
        public ActionResult PaymentRequest()
        {
            var paymentParam = new PaymentParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal
            };
            var paymentList = _paymentRepository.GetPaymentList(paymentParam);

            var viewModel = new PaymentViewModel
            {
                PaymentList = paymentList,
                BalBroughtForward = paymentParam.BalBroughtForward,
                MinAmount = paymentParam.MinAmount,
                PreferredPayment = paymentParam.PreferredPayment,
                PendingPayment = paymentList.FirstOrDefault(p => p.Status.MatchWith(PaymentStatus.Pending)),
                IsValidDateRange = IsValidDateRange()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            var resultInfo = new ResultInfo { IsOk = false };

            if (SessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }

            TempData[TempDataKey.PaymentResult] = resultInfo;

            return RedirectToAction("PaymentRequest");
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
                var affId = SessionService.UserSession.AffId;
                cancelParam.UserId = SessionService.UserSession.AffId;
                cancelParam.Language = SessionService.CurrentLanguage;
                cancelParam.Status = "Cancelled";
                cancelParam.Ip = Request.UserHostAddress;

                resultInfo = _paymentRepository.CancelPaymentRequest(cancelParam);

                if (resultInfo.IsOk)
                {
                    var userInfo = _accountRepository.GetUserInfoById(affId, SessionService.IsInternal);
                    if (!string.IsNullOrEmpty(userInfo.Email))
                    {
                        var emailparam = new PaymentEmailParam
                        {
                            User = userInfo,
                            PaymentType = PaymentRequestType.Cancel,
                            MailTo = userInfo.Email,
                            MailCc = AppSetting.MailCc,
                            MailContact = AppSetting.MailContact,
                            MailBcc = AppSetting.MailBcc,
                            RootDomain = SiteDomainHelper.GetRootUrl(this),
                            TransId = cancelParam.TransId,
                            Amount = cancelParam.Amount,
                            RequestDate = cancelParam.RequestDate
                        };

                        _emailService.SendPaymentEmail(emailparam);
                    }
                }
            }

            TempData[TempDataKey.PaymentResult] = resultInfo;

            return RedirectToAction("PaymentRequest");
        }

        private bool IsValidDateRange()
        {
            var currentDay = DateTime.Now.Day;
            return (currentDay >= BeginDay && currentDay <= EndDay) || AppSetting.IsLocal;
        }
    }
}