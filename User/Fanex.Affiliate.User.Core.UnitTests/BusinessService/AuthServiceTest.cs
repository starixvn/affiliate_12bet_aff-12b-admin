namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Commands;
    using Core.BusinessService;
    using Core.Utilities;
    using Criterias;
    using Data.Repository;
    using DTO.Auth;
    using DTO.VerificationAccount;
    using MetalModels;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Resources;
    using System;
    using Xunit;

    public class AuthServiceTest
    {
        private readonly IDynamicRepository _repository = Substitute.For<IDynamicRepository>();
        private readonly ICryptographyService _cryptographyService = Substitute.For<ICryptographyService>();
        private readonly IAppSetting _appSetting = Substitute.For<IAppSetting>();

        private const string Password = "Password";
        private const string UserName = "aff123";

        private static string LoginQueryString => $"u={UserName}&p={Password}";

        [Fact]
        public void GetLoginResult_WhenLoginWithInternalAdminUserWithNotExist_ReturnErrorResult()
        {
            var param = CreateLoginParam();
            var service = CreateService();
            _repository.Get<AuthorizedUserInfo>(Arg.Any<GetUserInfoCriteria>()).ReturnsNullForAnyArgs();

            var result = service.GetLoginResult(param, true);

            Assert.False(result.Result.IsOk);
            Assert.Equal(Error.LoginWrongInfo, result.Result.Message);
        }

        [Fact]
        public void GetLoginResult_WhenLoginWithInternalSuspendedAfUser_ReturnOK()
        {
            var param = CreateLoginParam();
            var userInfo = CreateSuspendedUserInfo();
            var service = CreateService();
            _repository.Get<AuthorizedUserInfo>(Arg.Any<GetUserInfoCriteria>()).Returns(userInfo);

            var result = service.GetLoginResult(param, true);

            Assert.True(result.Result.IsOk);
        }

        [Fact]
        public void GetLoginResult_WhenLoginInternalSuccess_ReturnOk()
        {
            var param = CreateLoginParam();
            var userInfo = CreateAuthorizedUserInfo();
            var service = CreateService();
            _repository.Get<AuthorizedUserInfo>(Arg.Any<GetUserInfoCriteria>()).Returns(userInfo);

            var result = service.GetLoginResult(param, true);

            Assert.True(result.Result.IsOk);
            Assert.Equal(userInfo, result.UserInfo);
        }

        [Fact]
        public void GetLoginResult_WhenLoginWithInvalidTimeStamp_ReturnFailResult()
        {
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            var nowTick = DateTimeOffset.UtcNow.AddHours(-1).ToUnixTimeMilliseconds();
            var loginQueryString = $"{LoginQueryString}&t={nowTick}";
            _cryptographyService.RsaDecrypt(null, null).ReturnsForAnyArgs(loginQueryString);

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.Contains(Error.YourSessionHasExpired, result.Result.Message);
        }

        [Fact]
        public void GetLoginResult_WhenQueryStringHaveEmptyUserName_ReturnFailResult()
        {
            var nowTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var queryString = $"u={string.Empty}&p={Password}&t={nowTick}";
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            _cryptographyService.RsaDecrypt(null, null).ReturnsForAnyArgs(queryString);

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.Contains(Lang.ErrmsgEntryUsername, result.Result.Message);
        }

        [Fact]
        public void GetLoginResult_WhenQueryStringHaveEmptyPassword_ReturnFailResult()
        {
            var nowTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var queryString = $"u={UserName}&p={string.Empty}&t={nowTick}";
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            _cryptographyService.RsaDecrypt(null, null).ReturnsForAnyArgs(queryString);

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.Contains(Lang.ErrmsgEntryPassword, result.Result.Message);
        }

        [Fact]
        public void GetLoginResult_WhenUserIsSuspended_ReturnFailResult()
        {
            var service = CreateService();
            var param = CreateLoginParam();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var user = CreateSuspendedUserInfo();
            _repository.Get<AuthorizedUserInfo>(Arg.Any<GetUserInfoCriteria>()).ReturnsForAnyArgs(user);

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.Contains(Error.YourAccountHasBeenSuspended, result.Result.Message);
        }

        [Fact]
        public void GetLoginResult_WhenUserVerifiedInfoNotExist_ReturnFailResult()
        {
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            param.IsVerifyingAccount = false;
            _repository.Get<VerificationInfo>(Arg.Any<GetLoginInfoByUserDeviceCriteria>()).ReturnsNullForAnyArgs();

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.True(result.Result.ShouldVerifyAccount);
        }

        [Fact]
        public void GetLoginResult_WhenUserVerifyFail_ReturnFailResult()
        {
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            param.IsVerifyingAccount = false;
            _repository.Get<VerificationInfo>(Arg.Any<GetLoginInfoByUserDeviceCriteria>()).ReturnsForAnyArgs(CreateFailVerificationInfo());

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.True(result.Result.ShouldVerifyAccount);
        }

        [Fact]
        public void GetLoginResult_WhenNotMatchPassword_ReturnFailResult()
        {
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            param.IsVerifyingAccount = false;
            _repository.Get<VerificationInfo>(Arg.Any<GetLoginInfoByUserDeviceCriteria>()).ReturnsForAnyArgs(CreateSuccessfulVerificationInfo());
            _cryptographyService.EncryptPassword(null).ReturnsForAnyArgs("abc");

            var result = service.GetLoginResult(param, false);

            Assert.False(result.Result.IsOk);
            Assert.Contains(Error.LoginWrongInfo, result.Result.Message);
        }

        [Fact]
        public void GetLoginResult_WhenLoginSuccess_ReturnOkResult()
        {
            var service = CreateService();
            SetUpLoginNormalUserPassedAllCheckCondition();
            var param = CreateLoginParam();
            var result = service.GetLoginResult(param, false);

            Assert.True(result.Result.IsOk);
        }

        [Fact]
        public void GetOnUserId_WhenCall_RepoCalled()
        {
            var service = CreateService();

            service.GetOnUserId(new OnUserIdParam());

            _repository.ReceivedWithAnyArgs().Execute(Arg.Any<CheckInCommand>());
        }

        [Fact]
        public void GetSessionId_WhenCall_RepoCalled()
        {
            var service = CreateService();

            service.GetSessionId(1);

            _repository.ReceivedWithAnyArgs().Get<string>(Arg.Any<GetSessionIdCriteria>());
        }

        [Fact]
        public void ReCheckin_WhenCall_RepoCalled()
        {
            var service = CreateService();

            service.Recheckin(1, false);

            _repository.ReceivedWithAnyArgs().Execute(Arg.Any<RecheckinCommand>());
        }

        private void SetUpLoginNormalUserPassedAllCheckCondition()
        {
            var nowTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var loginQueryString = $"{LoginQueryString}&t={nowTick}";
            _cryptographyService.RsaDecrypt(null, null).ReturnsForAnyArgs(loginQueryString);
            _appSetting.LoginTimeout.Returns(30);
            _repository.Get<AuthorizedUserInfo>(Arg.Any<GetUserInfoCriteria>()).ReturnsForAnyArgs(CreateAuthorizedUserInfo());
            _cryptographyService.EncryptPassword(null).ReturnsForAnyArgs("password");
            _repository.Execute(Arg.Any<UpdateLoginResultCommand>())
                .Returns(0)
                .AndDoes(c =>
                {
                    var command = c.Arg<UpdateLoginResultCommand>();
                    command.ErrCode = 0;
                    command.ErrMsg = string.Empty;
                });
        }

        private static VerificationInfo CreateFailVerificationInfo()
            => new VerificationInfo
            {
                DeviceCount = 11,
                FailedLoginCount = null
            };

        private static VerificationInfo CreateSuccessfulVerificationInfo()
            => new VerificationInfo
            {
                DeviceCount = 11,
                FailedLoginCount = 1
            };


        private static UserLoginParam CreateLoginParam()
            => new UserLoginParam
            {
                LoginInfo = "Chrome",
                IsVerifyingAccount = true,
                UserName = "test",
                Language = "vn-vi",
                LoginIp = "127.0.0.1",
                Password = "password",
                UserAgent = "Asia",
                ValidationKey = "1234"
            };

        private static AuthorizedUserInfo CreateAuthorizedUserInfo()
            => new AuthorizedUserInfo
            {
                AffId = 1,
                AffType = "Asia",
                GroupName = "group",
                OnUserId = 1,
                Password = "password",
                PreferredLanguage = "vn-vi",
                Status = "Approved",
                UserName = "test"
            };

        private static AuthorizedUserInfo CreateSuspendedUserInfo()
            => new AuthorizedUserInfo
            {
                AffId = 1,
                AffType = "Asia",
                GroupName = "group",
                OnUserId = 1,
                Password = "password",
                PreferredLanguage = "vn-vi",
                Status = "Suspended",
                UserName = "test"
            };

        private AuthService CreateService()
            => new AuthService(_repository, _cryptographyService, _appSetting);
    }
}
