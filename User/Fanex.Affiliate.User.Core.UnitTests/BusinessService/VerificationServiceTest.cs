namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Commands;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.VerificationAccount;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Utilities;
    using MetalModels;
    using NSubstitute;
    using System;
    using System.Security.Cryptography;
    using TestHelper;
    using Xunit;

    public class VerificationServiceTest
    {
        private readonly ITwoFactorAuthService TwoFactorAuthService;
        private readonly IAccountService AccountService;
        private readonly IEmailService MailService;
        private readonly ICryptographyService CryptoService;
        private readonly IWrapperUtility WrapperUtility;
        private readonly IDynamicRepository DynamicRepository;

        public VerificationServiceTest()
        {
            DynamicRepository = Substitute.For<IDynamicRepository>();
            TwoFactorAuthService = Substitute.For<ITwoFactorAuthService>();
            AccountService = Substitute.For<IAccountService>();
            MailService = Substitute.For<IEmailService>();
            CryptoService = Substitute.For<ICryptographyService>();
            WrapperUtility = Substitute.For<IWrapperUtility>();
        }

        [Fact]
        public void SendCode_WhenInNotTimeToSendCode_ReturnFailSendCodeResult()
        {
            var service = CreateService();
            var param = CreateSendCodeParam();
            var now = DateTimeHelper.CreateDateTime(2020, 1, 1);
            var interval = 10;
            var lastSendTime = now.AddMinutes(-5);
            param.SentCodeTime = lastSendTime.Ticks;
            param.IntervalInMinute = interval;
            WrapperUtility.GetGmt8NowDateTime().Returns(now);

            var result = service.SendCode(param);

            Assert.False(result.CanSendCode);
        }

        [Fact]
        public void SendCode_WhenParamNotHaveSameEmailAsUser_ReturnInvalidEmail()
        {
            var service = CreateService();
            var param = CreateSendCodeParam();
            var userInfo = CreateUserInfo();
            userInfo.Email = "anxcj@com.vn";
            WrapperUtility.GetGmt8NowDateTime().Returns(DateTimeHelper.Gmt8Now);
            AccountService.GetUserInfoById(1, false).ReturnsForAnyArgs(userInfo);

            var result = service.SendCode(param);

            Assert.False(result.IsValidEmail);
        }

        [Fact]
        public void SendCode_WhenUserNotHaveSercretKey_GenSecretKeyForUser()
        {
            var service = CreateService();
            var param = CreateSendCodeParam();
            var userInfo = CreateUserInfo();
            userInfo.SecretKey = null;
            WrapperUtility.GetGmt8NowDateTime().Returns(DateTimeHelper.Gmt8Now);
            WrapperUtility.GetRandomGenerator().Returns(new Random());
            AccountService.GetUserInfoById(1, false).ReturnsForAnyArgs(userInfo);
            TwoFactorAuthService.GenerateHotp(null, 0).ReturnsForAnyArgs("12345");
            AccountService.CreateSecretKey(0, null).ReturnsForAnyArgs(101, 102);

            service.SendCode(param);

            AccountService.ReceivedWithAnyArgs(2).CreateSecretKey(0, null);
            Assert.False(string.IsNullOrWhiteSpace(userInfo.SecretKey));
        }

        [Fact]
        public void SendCode_WhenUserHaveSecretKey_NotGenSecretKey()
        {
            var service = CreateService();
            var param = CreateSendCodeParam();
            var userInfo = CreateUserInfo();
            WrapperUtility.GetGmt8NowDateTime().Returns(DateTimeHelper.Gmt8Now);
            WrapperUtility.GetRandomGenerator().Returns(new Random());
            AccountService.GetUserInfoById(1, false).ReturnsForAnyArgs(userInfo);
            TwoFactorAuthService.GenerateHotp(null, 0).ReturnsForAnyArgs("12345");

            service.SendCode(param);

            AccountService.DidNotReceiveWithAnyArgs().CreateSecretKey(0, null);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(0)]
        public void IsValidOtp_WhenLoginFail_UpdateVerificationInfo(int errCode)
        {
            MockingHelper.SetUpLogEngineEnviroment();
            var service = CreateService();
            var param = CreateVerificationOtpParam();
            var verificationInfo = new VerificationInfo
            {
                DeviceCount = 1,
                FailedLoginCount = 1
            };

            DynamicRepository.Get<VerificationInfo>(Arg.Any<GetLoginInfoByUserDeviceCriteria>()).ReturnsForAnyArgs(verificationInfo);
            DynamicRepository.Execute(Arg.Any<UpdateVerificationInfoCommand>())
                .Returns(0)
                .AndDoes(call =>
                {
                    var command = call.Arg<UpdateVerificationInfoCommand>();
                    command.ErrCode = errCode;
                });

            service.IsValidOtp(param);

            DynamicRepository.ReceivedWithAnyArgs().Execute(Arg.Any<UpdateVerificationInfoCommand>());
        }

        [Theory]
        [InlineData(100)]
        [InlineData(0)]
        public void IsValidOtp_WhenLoginOkAndUseValidOtp_InsertUserDeviceToRepo(int errCode)
        {
            MockingHelper.SetUpLogEngineEnviroment();
            var service = CreateService();
            var param = CreateVerificationOtpParam();
            var authMsg = param.LoginInfo + param.HashKey + param.Captcha + param.OtpCode;
            param.HashMessage = CryptographyHelper.SHA256Hash(authMsg, param.HashKey);
            var verificationInfo = new VerificationInfo
            {
                DeviceCount = 1,
                FailedLoginCount = null
            };

            DynamicRepository.Get<VerificationInfo>(Arg.Any<GetLoginInfoByUserDeviceCriteria>()).ReturnsForAnyArgs(verificationInfo);
            DynamicRepository.Execute(Arg.Any<InsertUserDeviceInfoCommand>())
                .Returns(0)
                .AndDoes(call =>
                {
                    var command = call.Arg<InsertUserDeviceInfoCommand>();
                    command.ErrCode = errCode;
                });
            TwoFactorAuthService.VerifyHotp(param.SecretKey, param.Counter, param.OtpCode).Returns(true);

            var result = service.IsValidOtp(param);

            DynamicRepository.ReceivedWithAnyArgs().Execute(Arg.Any<InsertUserDeviceInfoCommand>());
            Assert.True(result);
        }

        [Fact]
        public void IsInvalidOtp_WhenLoginOkButUseInvalidOtp_NotInsertUserDeviceToRepo()
        {
            var service = CreateService();
            var param = CreateVerificationOtpParam();
            var authMsg = param.LoginInfo + param.HashKey + param.Captcha + param.OtpCode;
            param.HashMessage = CryptographyHelper.SHA256Hash(authMsg, param.HashKey);
            var verificationInfo = new VerificationInfo
            {
                DeviceCount = 1,
                FailedLoginCount = null
            };

            TwoFactorAuthService.VerifyHotp(null, 0, null).ReturnsForAnyArgs(false);
            DynamicRepository.Get<VerificationInfo>(Arg.Any<GetLoginInfoByUserDeviceCriteria>()).ReturnsForAnyArgs(verificationInfo);

            var result = service.IsValidOtp(param);

            DynamicRepository.DidNotReceiveWithAnyArgs().Execute(Arg.Any<InsertUserDeviceInfoCommand>());
            Assert.False(result);
        }

        [Fact]
        public void CreateActionLink_WhenCall_ReturnCorrectValue()
        {
            var service = CreateService();
            var nowTick = 10_000_000;
            var expirationMin = 10;
            var queryString = "controller=VerificationAccount&action=VerifyAccount&t=" + nowTick + "&e=" + expirationMin;
            var link = ConverterUtils.Base64Encode(SymmetricEncryptionUtility.Encrypt<AesManaged>(queryString, "abc", "12233333"));
            WrapperUtility.GetGmt8NowDateTime().ReturnsForAnyArgs(new DateTime(nowTick));

            var result = service.CreateActionLink("abc", "12233333", expirationMin);

            Assert.Equal(link, result.Link);
            Assert.Equal(nowTick.ToString(), result.TimeToken);

        }

        private static VerificationOtpParam CreateVerificationOtpParam()
            => new VerificationOtpParam
            {
                AffId = 1,
                Captcha = "1234",
                Counter = 1,
                DeviceInfo = "device",
                HashKey = "1234",
                HashMessage = "1234",
                IpAddress = "127.0.0.1",
                Language = "vi-VN",
                LoginInfo = "abc",
                OtpCode = "12345",
                SecretKey = "12345",
                UserAgent = "chrome"
            };

        private static UserInfo CreateUserInfo()
            => new UserInfo
            {
                AffId = 1,
                Email = "abc@gamail.com",
                FirstName = "first",
                LastName = "last",
                MailOption = false,
                PreferredLanguage = "en-UK",
                SecretKey = "1234",
                UserName = "username"
            };

        private static SendCodeParam CreateSendCodeParam()
            => new SendCodeParam
            {
                AffId = 1,
                CurrentLanguage = "en",
                Email = "abc@gamail.com",
                IntervalInMinute = 0,
                SentCodeTime = 10000,
                VerificationEmailParam = new MetalModels.Email.VerificationAccountEmailParam()
            };

        private VerificationService CreateService()
            => new VerificationService(DynamicRepository, TwoFactorAuthService, MailService, CryptoService, WrapperUtility, AccountService);
    }
}
