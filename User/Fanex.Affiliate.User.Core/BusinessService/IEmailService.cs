using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IEmailService
    {
        void SendRecoverPasswordEmail(RecoverPasswordEmailParam emailParam);

        void SendNewPasswordEmail(NewPasswordEmailParam emailParam);

        void SendPaymentEmail(PaymentEmailParam emailParam);

        void SendSignUpEmail(EmailBaseParam emailParam);

        void SendNoticeEmailToCsd(NoticeEmailParam emailParam);

        void SendContactUsEmail(EmailBaseParam emailParam, ContactUsParam model);

        void SendVerificationEmail(VerificationAccountEmailParam emailParam);
    }
}
