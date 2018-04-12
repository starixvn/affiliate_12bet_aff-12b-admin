using System.Web;
using Fanex.Affiliate.User.Core.DTO.Auth;

namespace Fanex.Affiliate.User.Core.CoreService
{
    public class SessionService : ISessionService
    {
        private const string UserSessionKey = "Affiliate12Bet.UserSessionKey";
        private const string CaptchaCodeKey = "Affiliate12Bet.CaptchaCodeKey";

        public string CaptchaCode
        {
            get { return HttpContext.Current.Session[CaptchaCodeKey] as string; }
            set { HttpContext.Current.Session[CaptchaCodeKey] = value; }
        }

        public UserSession UserSession
        {
            get
            {
                var temp = HttpContext.Current.Session[UserSessionKey];
                return temp != null ? (UserSession)temp : null;
            }
        }

        public void AssignSessions(AuthorizedUserInfo authorizedUser)
        {
            var userSession = new UserSession
            {
                AffId = authorizedUser.AffId,
                UserName = authorizedUser.UserName,
                PreferredLanguage = authorizedUser.PreferredLanguage
            };

            HttpContext.Current.Session[UserSessionKey] = userSession;
        }
    }
}
