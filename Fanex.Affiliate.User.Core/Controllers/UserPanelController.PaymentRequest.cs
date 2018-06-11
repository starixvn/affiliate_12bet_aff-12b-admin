namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using Extensions;
    using Filters;
    using MetalModels;
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

            var currentDay = DateTime.Now.Day;

            var viewModel = new PaymentViewModel
            {
                PaymentList = paymentList,
                BalBroughtForward = paymentParam.BalBroughtForward,
                MinAmount = paymentParam.MinAmount,
                PreferredPayment = paymentParam.PreferredPayment,
                PendingPayment = paymentList.FirstOrDefault(p => p.Status.MatchWith(PaymentStatus.Pending)),
                IsValidDateRange = (currentDay >= BeginDay && currentDay <= EndDay) || AppSetting.IsLocal
            };

            return View(viewModel);
        }
    }
}