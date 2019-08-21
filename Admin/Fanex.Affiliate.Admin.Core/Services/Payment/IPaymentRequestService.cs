using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;

namespace Fanex.Affiliate.Admin.Core.Services.Payment
{
    public interface IPaymentRequestService
    {
        EntityResult<PaymentRequest> GetPaymentRequestList(PaymentRequestParam param);

        PaymentRequestDetail GetPaymentRequestDetail(long transId);

        ResultInfo UpdatePaymentRequest(PaymentRequestUpdateParam param, PaymentRequestUpdateEmail emailParam);
    }
}
