using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
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

        public ResultInfo CancelPaymentRequest(PaymentCancelParam cancelParam)
        {
            const string uSpKey = "Aff_User_PaymentRequest_Upd";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                objectDb.DB.ExecuteNonQuery(cancelParam);
            }

            return new ResultInfo
            {
                IsOk = cancelParam.ErrCode == 0,
                Message = cancelParam.ErrMsg
            };
        }

        public ResultInfo CreatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            const string uSpKey = "Aff_PaymentRequestIns";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                objectDb.DB.ExecuteNonQuery(paymentCreationParam);
            }

            return new ResultInfo
            {
                IsOk = paymentCreationParam.ErrCode == 0,
                Message = paymentCreationParam.ErrMsg
            };
        }
    }
}