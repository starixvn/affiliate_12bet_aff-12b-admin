using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public interface ISessionService
    {
        IList<UserPermission> UserPerissionList { get; set; }
    }
}
