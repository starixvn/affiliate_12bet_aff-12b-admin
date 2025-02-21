using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ISessionService
    {
        int UserId { get; set; }

        int OnUserId { get; set; }

        string AffName { get; set; }

        IList<UserPermission> UserPermissionList { get; set; }

        bool HasHostPermission(string functionName);

        IEnumerable<ClaimsIdentity> ClaimIdentities { get; set; }

        ClaimsPrincipal UserClaim { get; }
    }
}