using System.Collections.Generic;
using System.Web;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class SessionService : ISessionService
    {
        private const string UserPermissionSessionKey = "__UserPermissionSessionKey";

        IList<UserPermission> ISessionService.UserPerissionList
        {

            get
            {
                var value = HttpContext.Current.Session[UserPermissionSessionKey];
                return value != null ? value as IList<UserPermission> : new List<UserPermission>();
            }
            set
            {
                HttpContext.Current.Session[UserPermissionSessionKey] = value;
            }
        }

    }
}
