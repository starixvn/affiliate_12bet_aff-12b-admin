using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface ISignInService
    {
        ResultInfo VerifyAccount(LoginParam param);

        bool IsKickOut(int trackId);

        void ReleaseTracking(int trackId);

        int TrackLoginInfo(LoginParam param, string sessionId, string serverId);
    }
}
