using System.Collections.Generic;
using System.Web;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class SessionService : ISessionService
    {
        private const string UserPermissionSessionKey = "__UserPermissionSessionKey";

        private const string UserIdKey = "__CustID";
        private const string OnUserIDKey = "__OnUserIDKey";
        private const string AffIdKey = "AffId";
        private const string AffNameKey = "AffName";

        public int UserId
        {
            get
            {
                var value = HttpContext.Current.Session[UserIdKey];
                return value != null ? (int)value : 0;
            }
            set
            {
                HttpContext.Current.Session[UserIdKey] = value;
            }
        }

        public int OnUserId
        {
            get
            {
                var value = HttpContext.Current.Session[OnUserIDKey];
                return value != null ? (int)value : 0;
            }
            set
            {
                HttpContext.Current.Session[OnUserIDKey] = value;
            }
        }

        public int AffId
        {
            get
            {
                var temp = HttpContext.Current.Session[AffIdKey];

                return temp == null ? 0 : (int)temp;
            }
            set
            {
                HttpContext.Current.Session["AffUsername"] = value;
            }
        }

        public string AffName
        {
            get
            {
                var temp = HttpContext.Current.Session[AffNameKey];

                return temp == null ? string.Empty : (string)temp;
            }
            set
            {
                HttpContext.Current.Session[AffNameKey] = value;
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
