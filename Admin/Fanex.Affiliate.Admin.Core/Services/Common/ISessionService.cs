using System.Collections.Generic;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ISessionService
    {
        int UserId { get; set; }

        int OnUserId { get; set; }

        int AffId { get; set; }

        string AffName { get; set; }

        IEnumerable<ClaimsIdentity> ClaimIdentities { get; set; }

        ClaimsPrincipal UserClaim { get; }
    }
}