using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
