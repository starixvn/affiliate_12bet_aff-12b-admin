using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetPaymentList(PaymentParam paymentParam);
    }
}
