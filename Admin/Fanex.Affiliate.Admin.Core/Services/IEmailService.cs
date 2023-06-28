using Fanex.Affiliate.Admin.Core.DTO.Email;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface IEmailService
    {
        void SendApprovedEmail(AffInformationEmail emailParam);

        void SendDisapprovedEmail(AffInformationEmail emailParam);

        void SendApprovedPaymentRequestEmail(PaymentRequestUpdateEmail emailParam);

        void SendRejectedPaymentRequestEmail(PaymentRequestUpdateEmail emailParam);
    }
}
