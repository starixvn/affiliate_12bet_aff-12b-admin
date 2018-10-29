using System.Web;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAppSetting _appSetting;

        public AuthService(IAuthRepository authRepository, ICryptographyService cryptographyService, IAppSetting appSetting)
        {
            _authRepository = authRepository;
            _cryptographyService = cryptographyService;
            _appSetting = appSetting;
        }

        public LoginResultViewModel GetLoginResult(UserLoginParam userParam, bool isInternal)
        {
            var user = _authRepository.GetUserInfo(userParam);
            if (user == null || user.IsClosed)
            {
                var message = user == null ? Error.LoginWrongInfo : Error.LoginClosedAccount;
                return new LoginResultViewModel
                {
                    Result = new ResultInfo { IsOk = false, Message = message }
                };
            }

            var loginResultParam = GetLoginResultParam(userParam, user, isInternal);

            _authRepository.UpdateLoginResult(loginResultParam);

            return new LoginResultViewModel
            {
                UserInfo = user,
                Result = new ResultInfo
                {
                    IsOk = loginResultParam.IsSuccessful && loginResultParam.ErrCode == 0,
                    Message = loginResultParam.IsSuccessful ? loginResultParam.ErrMsg : Error.LoginWrongInfo
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
                string encryptedPassword = _cryptographyService.EncryptPassword(_appSetting.RsaPrivateKey, userParam.Password);
                isMatchPassword = encryptedPassword.Equals(userInfo.Password);
            }

            return new LoginResultParam
            {
                AffId = userInfo.AffId,
                LoginIp = userParam.LoginIp,
                SessionId = HttpContext.Current.Session.SessionID,
                Language = userParam.Language,
                IsSuccessful = isInternal || isMatchPassword,
                IsInternal = userParam.IsInternal
            };
        }
    }
}