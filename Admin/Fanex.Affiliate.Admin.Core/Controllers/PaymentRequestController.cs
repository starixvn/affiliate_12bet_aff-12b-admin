using Fanex.Affiliate.Admin.Core.Repository.Payment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class PaymentRequestController : BaseController
    {
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        public PaymentRequestController(
            ISessionService sessionService,
            IPaymentRequestRepository paymentRequestRepository) : base(sessionService)
        {
            _paymentRequestRepository = paymentRequestRepository;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/PaymentRequest/Index.cshtml");
        }
    }
}
