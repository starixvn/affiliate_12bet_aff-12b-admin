using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Moq;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.UnitTests.TestHelper
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService()
        {
            return Mock.Of<ISessionService>(ss => 
                ss.AffId == Constant.AffId &&
                ss.AffName == Constant.AffName &&
                ss.OnUserId == Constant.OnUserId &&
                ss.UserId == Constant.UserId && 
                ss.UserPermissionList == new List<UserPermission>
                {
                    new UserPermission
                    {
                        IsHost = true,
                        FunctionName = Function.CommissionGroup
                    }
                }
            );
        }
    }
}
