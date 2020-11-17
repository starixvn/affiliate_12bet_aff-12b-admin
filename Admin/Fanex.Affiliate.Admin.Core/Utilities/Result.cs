using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class Result
    {
        public static ResultInfo Success => new ResultInfo { Code = Code.Success };

        public static ResultInfo NotHavePermissionOnAff => new ResultInfo { Code = Code.ErrCodeAccessDenied, Message = "You don't have permission on this affiliate" };

    }
}
