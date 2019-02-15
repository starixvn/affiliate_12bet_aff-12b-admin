using Fanex.Affiliate.User.Core.DTO.VerificationAccount;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class VerificationAccountViewModel
    {
        public string Email { get; set; }

        public VerificationAction Action { get; set; }
    }
}
