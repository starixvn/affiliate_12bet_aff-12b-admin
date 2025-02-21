namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System;
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Security;
    using Commands;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.Auth;
    using DTO.VerificationAccount;
    using MetalModels;
    using Resources;
    using Utilities;
    using ViewModels;

    public class AuthService : BaseDynamicService, IAuthService
    {
        private const int MaxFailedLoginCount = 5;
        private const int AllowedUserDeviceCount = 10;

        private readonly ICryptographyService _cryptographyService;
        private readonly IAppSetting _appSetting;

        public AuthService(
            IDynamicRepository dynamicRepository,
            ICryptographyService cryptographyService,
            IAppSetting appSetting) : base(dynamicRepository)
        {
            _cryptographyService = cryptographyService;
            _appSetting = appSetting;
        }

        public LoginResultViewModel GetLoginResult(UserLoginParam userParam, bool isInternal)
        {
            return isInternal ? GetLoginResultForInternalAdmin(userParam) : GetLoginResultForAffUser(userParam);
        }

        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            var command = onUserIdParam.MapTo<OnUserIdParam, CheckInCommand>();
            ExecuteCommand(command);

            return command.On_UserId;
        }

        public string GetSessionId(int onUserId)
        {
            return GetTData<string>(new GetSessionIdCriteria(onUserId));
        }

        public bool Recheckin(int onUserId, bool isInternal)
        {
            var command = new RecheckinCommand(onUserId, isInternal);
            ExecuteCommand(command);

            return command.IsKickedOut;
        }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        private LoginResultViewModel GetLoginResultForInternalAdmin(UserLoginParam userParam)
        {
            var user = GetTData<AuthorizedUserInfo>(new GetUserInfoCriteria(userParam.UserName, true));
            var checkingActiveUserResult = CheckActiveUser(user, true);

            return new LoginResultViewModel
            {
                UserInfo = user,
                Result = checkingActiveUserResult
            };
        }

        private LoginResultViewModel GetLoginResultForAffUser(UserLoginParam userParam)
        {
            var loginInfoQueryString = _cryptographyService.RsaDecrypt(_appSetting.RsaPrivateKey, userParam.LoginInfo);
            var loginInfo = HttpUtility.ParseQueryString(loginInfoQueryString);
            var queryStringValidationResult = IsValidQueryStringInfo(loginInfo);
            if (!queryStringValidationResult.Result.IsOk)
            {
                return queryStringValidationResult;
            }

            userParam.UserName = loginInfo["u"];
            var user = GetTData<AuthorizedUserInfo>(new GetUserInfoCriteria(userParam.UserName, false));
            var checkingActiveUserResult = CheckActiveUser(user);
            if (!checkingActiveUserResult.IsOk)
            {
                return new LoginResultViewModel { Result = checkingActiveUserResult };
            }

            var hashedDeviceInfo = _cryptographyService.ComputeSha256Hash(Parser.GetDeviceInfo(userParam.UserAgent));
            if (!userParam.IsVerifyingAccount)
            {
                var verificationInfo = GetTData<VerificationInfo>(new GetLoginInfoByUserDeviceCriteria
                {
                    AffId = user.AffId, 
                    IpAddress = userParam.LoginIp, 
                    DeviceInfo = hashedDeviceInfo
                });

                if (verificationInfo == null || ShouldVerifyAccount(verificationInfo))
                {
                    return new LoginResultViewModel
                    {
                        UserInfo = user,
                        Result = new LoginResultInfo { IsOk = false, ShouldVerifyAccount = true }
                    };
                }
            }

            userParam.Password = loginInfo["p"];
            var isMatchPassword = IsMatchPassword(userParam.Password, user.Password);
            var loginResultParam = CreateLoginResultParam(userParam, isMatchPassword, user.AffId, hashedDeviceInfo);
            var loginResultInfo = ExecuteReturnResultInfo(loginResultParam.MapTo<LoginResultParam, UpdateLoginResultCommand>());

            var isSuccessful = loginResultInfo.IsOk && isMatchPassword;

            return new LoginResultViewModel
            {
                UserInfo = user,
                Result = new LoginResultInfo { IsOk = isSuccessful, Message = isSuccessful ? string.Empty : Error.LoginWrongInfo }
            };
        }

        private static LoginResultInfo CheckActiveUser(AuthorizedUserInfo user, bool isInternal = false)
        {
            const string Approved = "Approved";
            const string Suspended = "Suspended";
            if (user == null)
            {
                return new LoginResultInfo { IsOk = false, Message = Error.LoginWrongInfo };
            }

            user.IsMaxSubLevel = true;
            if (!string.Equals(Approved, user.Status, StringComparison.CurrentCultureIgnoreCase))
            {
                if (isInternal && string.Equals(Suspended, user.Status, StringComparison.CurrentCultureIgnoreCase))
                {
                    return new LoginResultInfo { IsOk = true };
                }

                return new LoginResultInfo { IsOk = false, Message = Error.YourAccountHasBeenSuspended };
            }

            return new LoginResultInfo { IsOk = true };
        }

        private static bool ShouldVerifyAccount(VerificationInfo verificationInfo)
        {
            var failedLoginCount = verificationInfo.FailedLoginCount;

            return failedLoginCount >= MaxFailedLoginCount || (!failedLoginCount.HasValue && verificationInfo.DeviceCount > AllowedUserDeviceCount);
        }

        private LoginResultViewModel IsValidQueryStringInfo(NameValueCollection loginInfo)
        {
            var timeStampString = loginInfo["t"];
            if (!IsValidTimeStamp(timeStampString, _appSetting.LoginTimeout))
            {
                return new LoginResultViewModel
                {
                    Result = new LoginResultInfo { IsOk = false, Message = Error.YourSessionHasExpired }
                };
            }

            var userName = loginInfo["u"];
            var password = loginInfo["p"];
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new LoginResultViewModel
                {
                    Result = new LoginResultInfo
                    {
                        IsOk = false,
                        Message = string.IsNullOrEmpty(userName) ? Lang.ErrmsgEntryUsername : Lang.ErrmsgEntryPassword
                    }
                };
            }

            return new LoginResultViewModel
            {
                Result = new LoginResultInfo { IsOk = true }
            };
        }

        private bool IsMatchPassword(string passwordInput, string passwordFromDb)
        {
            var encryptedPassword = _cryptographyService.EncryptPassword(passwordInput);

            return encryptedPassword.Equals(passwordFromDb);
        }

        private static LoginResultParam CreateLoginResultParam(UserLoginParam userParam, bool isSuccessful, int affId, string deviceInfo = null)
        {
            return new LoginResultParam
            {
                AffId = affId,
                IpAddress = userParam.LoginIp,
                Language = userParam.Language,
                IsSuccessful = isSuccessful,
                DeviceInfo = deviceInfo
            };
        }

        private static bool IsValidTimeStamp(string timeStampString, int timeout)
        {
            var timestamp = Convert.ToInt64(timeStampString);
            var utcDateTimeRequest = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
            var utcNow = DateTimeHelper.UtcNow;

            return DateTime.Compare(utcDateTimeRequest, utcNow.AddMinutes(timeout * -1)) >= 0;
        }
    }
}