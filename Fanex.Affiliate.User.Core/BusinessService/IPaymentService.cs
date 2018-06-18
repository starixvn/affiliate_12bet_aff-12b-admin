using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetPaymentList(PaymentParam paymentParam);

        ResultInfo CreatePaymentRequest(PaymentCreationParam paymentCreationParam, PaymentEmailParam emailParam);

        ResultInfo CancelPaymentRequest(PaymentCancelParam cancelParam, PaymentEmailParam emailParam);

        bool IsInDateRange();
    }
}
