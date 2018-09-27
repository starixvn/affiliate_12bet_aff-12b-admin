using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetPaymentList(PaymentParam paymentParam);

        ResultInfo CancelPaymentRequest(PaymentCancelParam cancelParam);

        ResultInfo CreatePaymentRequest(PaymentCreationParam paymentCreationParam);
    }
}
