using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;

namespace Fanex.Affiliate.User.Core.CoreService
{
    public class SessionService : ISessionService
    {
        private const string UserSessionKey = "Affiliate12Bet.UserSessionKey";
        private const string CaptchaCodeKey = "Affiliate12Bet.CaptchaCodeKey";
        private const string SingUpCaptchaCodeKey = "Affiliate12Bet.SingUpCaptchaCodeKey";

        private const string CurrentLanguageKey = "Affiliate12Bet.CurrentLanguageKey";
        private const string IsInternalKey = "Affiliate12Bet.IsInternalKey";

        private const string RsaKeyPairKey = "Affiliate12Bet.RsaKeyPairKey";
        private const string VerificationSessionKey = "Affiliate12Bet.VerificationSessionKey";
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

        public string CurrentLanguage
        {
            get
            {
                var session = HttpContext.Current.Session;
                if (session == null)
                {
                    return SystemLanguage.DefaultLanguageCode;
                }

                var currentLanguage = session[CurrentLanguageKey] as string;

                return string.IsNullOrEmpty(currentLanguage) ? SystemLanguage.DefaultLanguageCode : currentLanguage;
            }
            set
            {
                HttpContext.Current.Session[CurrentLanguageKey] = value;
            }
        }

        public bool IsInternal
        {
            get { return (bool)(HttpContext.Current.Session[IsInternalKey] ?? false); }
            set { HttpContext.Current.Session[IsInternalKey] = value; }
        }

        public string SignUpCaptchaCode
        {
            get { return HttpContext.Current.Session[SingUpCaptchaCodeKey] as string; }
            set { HttpContext.Current.Session[SingUpCaptchaCodeKey] = value; }
        }

        public string PrivateKey
        {
            get
            {
                return HttpContext.Current.Session[RsaKeyPairKey] as string;
            }
            set
            {
                HttpContext.Current.Session[RsaKeyPairKey] = value;
            }
        }

        public void AssignSessions(AuthorizedUserInfo authorizedUser)
        {
            var userSession = new UserSession
            {
                AffId = authorizedUser.AffId,
                UserName = authorizedUser.UserName,
                PreferredLanguage = string.IsNullOrEmpty(authorizedUser.PreferredLanguage) ?
                                        SystemLanguage.DefaultLanguageCode :
                                        authorizedUser.PreferredLanguage,
                OnUserId = authorizedUser.OnUserId,
                AffType = authorizedUser.AffType,
                GroupName = authorizedUser.GroupName
            };

            HttpContext.Current.Session[UserSessionKey] = userSession;
        }

        public VerificationSession VerificationSession
        {
            get { return HttpContext.Current.Session[VerificationSessionKey] as VerificationSession; }
            set { HttpContext.Current.Session[VerificationSessionKey] = value; }
        }
    }
}