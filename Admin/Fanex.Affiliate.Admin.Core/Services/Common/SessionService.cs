using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using static Fanex.Affiliate.Admin.Core.Constants.SessionKey;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class SessionService : ISessionService
    {
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
                HttpContext.Current.Session[AffIdKey] = value;
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

        public IEnumerable<ClaimsIdentity> ClaimIdentities
        {
            get => HttpContext.Current.Session[ClaimSessionKey] as IEnumerable<ClaimsIdentity>;
            set => HttpContext.Current.Session[ClaimSessionKey] = value;
        }

        public ClaimsPrincipal UserClaim => HttpContext.Current.User as ClaimsPrincipal;

        public string RequestIp
        {
            get
            {
                var request = HttpContext.Current.Request;
                string ipList = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipList))
                {
                    return ipList.Split(',')[0];
                }

                return request.ServerVariables["REMOTE_ADDR"];
            }
        }
    }
}
