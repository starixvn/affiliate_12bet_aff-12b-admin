using System;
using System.Collections.Specialized;
using System.Web;
using Fanex.Affiliate.User.Core.DTO;
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
            var loginInfoQueryString = _cryptographyService.RsaDecrypt(_appSetting.RsaPrivateKey, userParam.LoginInfo);
            var loginInfo = HttpUtility.ParseQueryString(loginInfoQueryString);
            var queryStringValidationResult = IsValidQueryStringInfo(loginInfo);
            if (!queryStringValidationResult.Result.IsOk)
            {
                return queryStringValidationResult;
            }

            userParam.UserName = loginInfo["u"];
            var user = _authRepository.GetUserInfo(userParam.UserName, isInternal);
            if (user == null || user.IsClosed)
            {
                var message = user == null ? Error.LoginWrongInfo : Error.LoginClosedAccount;
                return new LoginResultViewModel
                {
                    Result = new ResultInfo { IsOk = false, Message = message }
                };
            }

            userParam.Password = loginInfo["p"];
            var isMatchPassword = isInternal || IsMatchPassword(userParam.Password, user.Password);
            var loginResultInfo = isMatchPassword ? _authRepository.UpdateLoginResult(GetLoginResultParam(userParam, user.AffId), isInternal) :
                                                    new ResultInfo { IsOk = false, Message = Error.LoginWrongInfo };

            return new LoginResultViewModel
            {
                UserInfo = user,
                Result = loginResultInfo
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

        private LoginResultViewModel IsValidQueryStringInfo(NameValueCollection loginInfo)
        {
            var timeStampString = loginInfo["t"];
            if (!IsValidTimeStamp(timeStampString, _appSetting.LoginTimeout))
            {
                return new LoginResultViewModel
                {
                    Result = new ResultInfo { IsOk = false, Message = Error.YourSessionHasExpired }
                };
            }

            var userName = loginInfo["u"];
            var password = loginInfo["p"];
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new LoginResultViewModel
                {
                    Result = new ResultInfo
                    {
                        IsOk = false,
                        Message = string.IsNullOrEmpty(userName) ? Lang.ErrmsgEntryUsername : Lang.ErrmsgEntryPassword
                    }
                };
            }

            return new LoginResultViewModel
            {
                Result = new ResultInfo { IsOk = true }
            };
        }

        private bool IsMatchPassword(string passwordInput, string passwordFromDb)
        {
            if (string.IsNullOrEmpty(passwordInput))
            {
                return false;
            }

            string encryptedPassword = _cryptographyService.EncryptPassword(passwordInput);

            return encryptedPassword.Equals(passwordFromDb);
        }

        private LoginResultParam GetLoginResultParam(UserLoginParam userParam, int affId)
        {
            return new LoginResultParam
            {
                AffId = affId,
                LoginIp = userParam.LoginIp,
                SessionId = HttpContext.Current.Session.SessionID,
                Language = userParam.Language,
                IsSuccessful = true
            };
        }

        private static bool IsValidTimeStamp(string timeStampString, int timeout)
        {
            var timestamp = Convert.ToInt64(timeStampString);
            var utcDateTimeRequest = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
            var utcNow = DateTime.Now.ToUniversalTime();

            return DateTime.Compare(utcDateTimeRequest, utcNow.AddMinutes(timeout * -1)) >= 0;
        }
    }
}