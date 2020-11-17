
using System.Collections.Generic;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface IAuthService
    {
        IEnumerable<Claim> GetClaims(int userId);

        bool IsUserHasPermissionOnAff(int userId, int affId);
    }
}
