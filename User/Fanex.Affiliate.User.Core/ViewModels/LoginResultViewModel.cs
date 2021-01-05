using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class LoginResultViewModel
    {
        public AuthorizedUserInfo UserInfo { get; set; }

        public LoginResultInfo Result { get; set; }
    }
}
