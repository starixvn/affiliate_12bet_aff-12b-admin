using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ISessionService
    {
        int UserId { get; set; }

        int OnUserId { get; set; }

        int AffId { get; set; }

        string AffName { get; set; }

        IList<UserPermission> UserPermissionList { get; set; }
    }
}