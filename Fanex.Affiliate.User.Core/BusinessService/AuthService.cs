using System.Web;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ICookieService _cookieService;
        private readonly ICryptographyService _cryptographyService;

        public AuthService(IAuthRepository authRepository, ICookieService cookieService, ICryptographyService cryptographyService)
        {
            _authRepository = authRepository;
            _cookieService = cookieService;
            _cryptographyService = cryptographyService;
        }

        public LoginResultViewModel GetLoginResult(UserLoginParam userParam, bool isInternal)
        {
            var user = _authRepository.GetUserInfo(userParam);
            if (userParam.ErrorCode != 0)
            {
                return new LoginResultViewModel
                {
                    Result = new ResultInfo { Message = userParam.ErrorMessage }
                };
            }

            var loginResultParam = GetLoginResultParam(userParam, user, isInternal);

            _authRepository.UpdateLoginResult(loginResultParam);

            return new LoginResultViewModel
            {
                UserInfo = user,
                Result = new ResultInfo
                {
                    IsOk = loginResultParam.ErrorCode == 0,
                    Message = loginResultParam.ErrorMessage
                }
            };
        }

        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            return _authRepository.GetOnUserId(onUserIdParam);
        }

        public string GetSessionId(int onUserId)
        {
            return _authRepository.GetSessionId(onUserId);
        }

        public bool Recheckin(int onUserId, bool isInternal)
        {
            return _authRepository.Recheckin(onUserId, isInternal);
        }

        private LoginResultParam GetLoginResultParam(UserLoginParam userParam, AuthorizedUserInfo userInfo, bool isInternal)
        {
            var isMatchPassword = false;
            if (!isInternal)
            {
                string encryptedPassword = _cryptographyService.EncryptPassword(userParam.Password);
                isMatchPassword = encryptedPassword.Equals(userInfo.Password);
            }

            return new LoginResultParam
            {
                AffId = userInfo.AffId,
                ServerId = userParam.ServerId,
                LoginIp = userParam.LoginIP,
                SessionId = HttpContext.Current.Session.SessionID,
                Language = userParam.Language,
                IsSuccessful = isInternal || isMatchPassword,
                IsInternal = userParam.IsInternal
            };
        }
    }
}