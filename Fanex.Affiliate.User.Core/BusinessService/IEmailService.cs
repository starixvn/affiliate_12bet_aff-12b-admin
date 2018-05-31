using Fanex.Affiliate.User.Core.MetalModels.Email;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IEmailService
    {
        void SendRecoverPasswordEmail(RecoverPasswordEmailParam emailParam);
    }
}
