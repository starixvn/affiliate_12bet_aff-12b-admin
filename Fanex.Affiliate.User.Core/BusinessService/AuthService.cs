using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
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

        private LoginResultParam GetLoginResultParam(UserLoginParam userParam, AuthorizedUserInfo userInfo, bool isInternal)
        {
            var isMatchPassword = false;
            if (!isInternal)
            {
                string encryptedPassword = EncryptPassword(userParam.Password);
                isMatchPassword = encryptedPassword.Equals(userInfo.Password);
            }

            return new LoginResultParam
            {
                AffId = userInfo.AffId,
                ServerId = userParam.ServerId,
                LoginIp = userParam.LoginIP,
                SessionId = HttpContext.Current.Session.SessionID,
                Language = userParam.Language,
                IsSuccessful = isInternal || isMatchPassword
            };
        }

        private static string EncryptPassword(string inputPassword)
        {
            var encryptionKey = CookiesHelper.GetCookies(CookieName.EncryptionKey);
            var iv = CookiesHelper.GetCookies(CookieName.InitializationVector);

            var plainPassword = CryptographyHelper.DecryptWithAES(inputPassword, encryptionKey, iv);

            return CryptographyHelper.Encrypt(plainPassword);
        }
    }
}