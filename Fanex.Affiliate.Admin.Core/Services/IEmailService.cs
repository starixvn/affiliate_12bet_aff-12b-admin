using Fanex.Affiliate.Admin.Core.DTO.Email;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface IEmailService
    {
        void SendApprovedEmail(AffInformationEmail emailParam);
    }
}
