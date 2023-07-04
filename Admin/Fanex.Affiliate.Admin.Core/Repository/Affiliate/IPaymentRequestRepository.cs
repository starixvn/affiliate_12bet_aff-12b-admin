using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public interface IPaymentRequestRepository
    {
        EntityResult<PaymentRequest> GetPaymentRequestList(PaymentRequestParam param);

        PaymentRequestDetail GetPaymentRequestDetail(long transId);

        PaymentUpdateResult UpdatePaymentRequest(PaymentUpdateParam param);

        SiteDBConfigInfo GetSiteDBConfigInfoByCustId(string playerIbcCustId);

        ResultInfo ApprovePaymentRequest(PaymentApproveParam param);

        ResultInfo RollbackApprovePaymentRequest(ResultInfo approveResult, string detailXml);

        void WriteLogWhenUpdatePaymentRequestSuccessfully(PaymentUpdateLogParam param);
    }
}
