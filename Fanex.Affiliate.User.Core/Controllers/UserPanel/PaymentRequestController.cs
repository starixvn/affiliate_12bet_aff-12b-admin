namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using MetalModels.Email;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using Resources;
    using Utilities;
    using ViewModels.UserPanel;

    [CheckUMFilter]
    [CustomAuthorize]
    public class PaymentRequestController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IPaymentService _paymentService;
        private readonly IAppSetting _appSetting;
        public PaymentRequestController(ISessionService sessionService,
            IAppSetting appSetting,
            IPaymentService paymentService)
        {
            _sessionService = sessionService;
            _paymentService = paymentService;
            _appSetting = appSetting;
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            return View("~/Views/UserPanel/PaymentRequest/Index.cshtml",GetViewModel());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult PaymentRequestPartial()
        {
            return PartialView("~/Views/UserPanel/PaymentRequest/PaymentPartial/_Main.cshtml", GetViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            var resultInfo = new ResultInfo { IsOk = true };

            if (_sessionService.IsInternal)
            {
                resultInfo.IsOk = false;
                resultInfo.Message = Lang.ErrAuthorized;
            }

            if (resultInfo.IsOk)
            {
                paymentCreationParam.AffId = _sessionService.UserSession.AffId;
                paymentCreationParam.Language = _sessionService.CurrentLanguage;
                paymentCreationParam.Ip = Request.UserHostAddress;
                paymentCreationParam.IsInternal = _sessionService.IsInternal;
                paymentCreationParam.IsLocalTest = !_appSetting.IsProduction;

                var emailParam = new PaymentEmailParam
                {
                    MailCc = _appSetting.MailCc,
                    MailContact = _appSetting.MailContact,
                    MailBcc = _appSetting.MailBcc,
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

            if (_sessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }
            else
            {
                cancelParam.UserId = _sessionService.UserSession.AffId;
                cancelParam.Language = _sessionService.CurrentLanguage;
                cancelParam.Ip = Request.UserHostAddress;

                var emailParam = new PaymentEmailParam
                {
                    MailCc = _appSetting.MailCc,
                    MailContact = _appSetting.MailContact,
                    MailBcc = _appSetting.MailBcc,
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
                AffId = _sessionService.UserSession.AffId,
                IsInternal = _sessionService.IsInternal
            };
            var paymentList = _paymentService.GetPaymentList(paymentParam);

            var viewModel = new PaymentViewModel
            {
                PaymentList = paymentList,
                BalBroughtForward = paymentParam.BalBroughtForward,
                MinAmount = paymentParam.MinAmount,
                PreferredPayment = paymentParam.PreferredPayment,
                PendingPayment = paymentList.FirstOrDefault(p => p.Status.MatchWith(PaymentStatus.Pending)),
                IsValidDate = _paymentService.IsInDateRange() || !_appSetting.IsProduction
            };

            return viewModel;
        }
    }
}