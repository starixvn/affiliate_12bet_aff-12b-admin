using System.Collections.Generic;
using System.Web;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class SessionService : ISessionService
    {
        private const string UserPermissionSessionKey = "__UserPermissionSessionKey";

        private const string UserIdKey = "CustID";

        public int UserId
        {
            get
            {
                return (int)HttpContext.Current.Session[UserIdKey];
            }
            set
            {
                HttpContext.Current.Session[UserIdKey] = value;
            }
        }

        IList<UserPermission> ISessionService.UserPermissionList
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
