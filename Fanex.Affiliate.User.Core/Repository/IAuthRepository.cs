using Fanex.Affiliate.User.Core.DTO.Auth;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IAuthRepository
    {
        AuthorizedUserInfo Auth(string userName, string userPass, string loginIp, int serverId);
    }
}