using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Payment;

namespace Fanex.Affiliate.Admin.Core.Services.Payment
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IPaymentRequestRepository _paymentRequestRepository;

        public PaymentRequestService(IPaymentRequestRepository paymentRequestRepository)
        {
            _paymentRequestRepository = paymentRequestRepository;
        }

        public EntityResult<PaymentRequest> GetPaymentRequestList(PaymentRequestParam param)
        {
            return _paymentRequestRepository.GetPaymentRequestList(param);
        }

        public PaymentRequestDetail GetPaymentRequestDetail(long transId)
        {
            return _paymentRequestRepository.GetPaymentRequestDetail(transId);
        }

        public ResultInfo UpdatePaymentRequest(PaymentRequestUpdateParam param)
        {
            var result = _paymentRequestRepository.UpdatePaymentRequest(param);

            throw new NotImplementedException();
        }
    }
}
