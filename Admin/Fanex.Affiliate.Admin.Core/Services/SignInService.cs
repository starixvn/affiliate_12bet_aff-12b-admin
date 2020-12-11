using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class SignInService : ISignInService
    {
        private IAuthRepository AuthRepository { get; }

        public SignInService(IAuthRepository authRepository)
        {
            AuthRepository = authRepository;
        }

        public bool IsKickOut(int trackId)
            => AuthRepository.ReCheckin(trackId);

        public void ReleaseTracking(int trackId)
            => AuthRepository.Logout(trackId);

        public ResultInfo VerifyAccount(LoginParam param)
            => AuthRepository.Login(param);

        public int TrackLoginInfo(LoginParam param, string sessionId, string serverId)
            => AuthRepository.LoginOnUser(param, sessionId, serverId);
    }
}
