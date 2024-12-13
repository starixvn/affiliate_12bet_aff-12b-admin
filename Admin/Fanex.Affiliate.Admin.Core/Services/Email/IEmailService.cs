using Fanex.Affiliate.Admin.Core.DTO.Email;

namespace Fanex.Affiliate.Admin.Core.Services.Email
{
    public interface IEmailService
    {
        void SendApprovedEmail(AffInformationEmail emailParam);

        void SendDisapprovedEmail(AffInformationEmail emailParam);

        void SendPaymentRequestUpdateEmail(PaymentRequestUpdateEmail emailParam);
    }
}
