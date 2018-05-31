using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.MetalModels.Email
{
    public class RecoverPasswordEmailParam : EmailBaseParam
    {
        public UserInfo User { get; set; }

        public string Code { get; set; }
    }
}
