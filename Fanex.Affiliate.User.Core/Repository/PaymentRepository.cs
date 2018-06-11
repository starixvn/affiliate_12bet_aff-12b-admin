using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public IEnumerable<Payment> GetPaymentList(PaymentParam paymentParam)
        {
            const string uSpKey = "Aff_PaymentRequestSelInfoByAff";
            var mode = paymentParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, mode))
            {
                return objectDb.DB.Query<Payment>(paymentParam);
            }
        }
    }
}