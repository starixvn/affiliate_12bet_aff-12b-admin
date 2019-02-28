using Fanex.Affiliate.User.Core.DTO.VerificationAccount;

namespace Fanex.Affiliate.User.Core.DTO
{
    public class SendCodeResponse : ResultInfo
    {
        public string VerificationToken { get; set; }

        public VerificationAction Action { get; set; }
    }
}
