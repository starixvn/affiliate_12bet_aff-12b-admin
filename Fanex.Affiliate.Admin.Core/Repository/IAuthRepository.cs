using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAuthRepository
    {
        IList<UserPermission> GetPermissionList(int userId);
    }
}
