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
            var resultInfo = ValidatePaymentRequest(paymentCreationParam);

            if (resultInfo.IsOk)
            {
                var affId = SessionService.UserSession.AffId;
                var checkingResult = _accountService.CheckPlayerAccountIsActived(affId);
                if (checkingResult.IsOk)
                {
                    var creationParam = new PaymentCreationParam
                    {
                        AffId = affId,
                        Amount = paymentCreationParam.Amount,
                        Language = SessionService.CurrentLanguage,
                        Ip = Request.UserHostAddress
                    };

                    resultInfo = _paymentRepository.CreatePaymentRequest(creationParam);

                    if (resultInfo.IsOk)
                    {
                        SendEmail(
                            PaymentRequestType.Create,
                            creationParam.TransId,
                            creationParam.Amount,
                            creationParam.TransDate);
                    }
                }
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
                cancelParam.UserId = SessionService.UserSession.AffId;
                cancelParam.Language = SessionService.CurrentLanguage;
                cancelParam.Status = "Cancelled";
                cancelParam.Ip = Request.UserHostAddress;

                resultInfo = _paymentRepository.CancelPaymentRequest(cancelParam);

                if (resultInfo.IsOk)
                {
                    SendEmail(
                        PaymentRequestType.Cancel,
                        cancelParam.TransId,
                        cancelParam.Amount,
                        cancelParam.RequestDate);
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

        private bool IsValidAmount(PaymentCreationParam paymentCreationParam)
        {
            return paymentCreationParam.Amount > 0 &&
                paymentCreationParam.Amount >= paymentCreationParam.MinAmount &&
                paymentCreationParam.Amount <= paymentCreationParam.BalBroughtForward;
        }

        private void SendEmail(PaymentRequestType paymentType, long transId, decimal amount, DateTime transDate)
        {
            var userInfo = _accountRepository.GetUserInfoById(SessionService.UserSession.AffId, SessionService.IsInternal);
            if (!string.IsNullOrEmpty(userInfo.Email))
            {
                var emailparam = new PaymentEmailParam
                {
                    User = userInfo,
                    PaymentType = paymentType,
                    MailTo = userInfo.Email,
                    MailCc = AppSetting.MailCc,
                    MailContact = AppSetting.MailContact,
                    MailBcc = AppSetting.MailBcc,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    TransId = transId,
                    Amount = amount,
                    RequestDate = transDate
                };

                _emailService.SendPaymentEmail(emailparam);
            }
        }

        private ResultInfo ValidatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            var resultInfo = new ResultInfo { IsOk = false };

            if (SessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }
            else if (!IsValidDateRange())
            {
                resultInfo.Message = Lang.PayoutTime;
            }
            else if (!IsValidAmount(paymentCreationParam))
            {
                resultInfo.Message = Error.PaymentAmountInvalid;
            }
            else
            {
                resultInfo.IsOk = true;
            }

            return resultInfo;
        }
    }
}