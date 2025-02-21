namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using System.Threading.Tasks;
    using Apis;
    using Commands.Account;
    using Core.Constants.Apis;
    using Core.Utilities;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.Apis;
    using Fanex.Affiliate.User.Core.BusinessService;
    using MetalModels;
    using MetalModels.Email;
    using Moq;
    using Resources;
    using TestHelper;
    using Xunit;

    public class AccountServiceTest
    {
        private readonly Mock<IDynamicRepository> _repository;

        public AccountServiceTest()
        {
            _repository = new Mock<IDynamicRepository> { CallBase = true };
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CreateAffiliate_ValidDepositAccountInfo_CallRepositoryByEnvironment(bool isLocalTest)
        {
            // Arrange
            var signUpParam = new UserSignUpParam { IsLocal = isLocalTest };
            _repository.Setup(_ => _.Execute(It.IsAny<CreateAffiliateCommand>(), string.Empty)).Returns(1);

            var mockApi = new Mock<ILicenseeApi>();
            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                            .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.RUNNING });

            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            await accountService.CreateAffiliateAsync(signUpParam, new EmailSignUpParam());

            // Assert
            _repository.Verify(ac => ac.Execute(It.IsAny<CreateAffiliateCommand>(), string.Empty), Times.Once);
        }

        [Fact]
        public async Task CreateAffiliate_InvalidPlayerDepositInfo_DoNotCallRepository()
        {
            // Arrange
            const string PlayerUserName = "player";
            var signUpParam = new UserSignUpParam { Player_UserName = PlayerUserName };

            var accountService = CreateAccountService(_repository.Object);

            // Action
            await accountService.CreateAffiliateAsync(signUpParam, new EmailSignUpParam());

            // Assert
            _repository.Verify(ac => ac.Execute(It.IsAny<CreateAffiliateCommand>(), string.Empty), Times.Never);
        }

        [Fact]
        public async Task CreateAffiliate_WhenCallWithClosedPlayerDepositInfo_ReturnExpectedResult()
        {
            // Arrange
            var signUpParam = new UserSignUpParam { IsLocal = false };

            var mockApi = new Mock<ILicenseeApi>();
            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                            .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.CLOSED });

            var expectedResult = new ResultInfo { IsOk = false, Message = Lang.UserNameClosed };

            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            var result = await accountService.CreateAffiliateAsync(signUpParam, new EmailSignUpParam());

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public async Task CreateAffiliate_WhenCallWithSuspendedPlayerDepositInfo_ReturnExpectedResult()
        {
            // Arrange
            var signUpParam = new UserSignUpParam { IsLocal = false };

            var mockApi = new Mock<ILicenseeApi>();
            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                            .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.SUSPENDED });

            var expectedResult = new ResultInfo { IsOk = false, Message = Lang.UserNameSuspended };

            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            var result = await accountService.CreateAffiliateAsync(signUpParam, new EmailSignUpParam());

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public async Task CreateAffiliate_WhenCallWithInvalidPlayerDepositInfo_ReturnExpectedResult()
        {
            // Arrange
            var signUpParam = new UserSignUpParam { IsLocal = false };

            var mockApi = new Mock<ILicenseeApi>();
            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                            .ReturnsAsync(new MemberSiteCustomerInfo { Status = null });

            var expectedResult = new ResultInfo { IsOk = false, Message = Lang.errDepositDBConnectFailed };
            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            var result = await accountService.CreateAffiliateAsync(signUpParam, new EmailSignUpParam());

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public async Task CreateAffiliate_CreateSuccessfully_ReturnCorrectResult()
        {
            // Arrange
            var expectedResultInfo = new ResultInfo { IsOk = true, Message = string.Empty };
            var signUpParam = new UserSignUpParam { IsLocal = true };
            var mockApi = new Mock<ILicenseeApi>();
            _repository.Setup(_ => _.Execute(It.IsAny<CreateAffiliateCommand>(), string.Empty))
                .Returns(0)
                .Callback<CreateAffiliateCommand, string>((r, ip) =>
                {
                    r.ErrCode = 0;
                    r.ErrMsg = string.Empty;
                });

            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.RUNNING, CurrencyId = 123 });

            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            var result = await accountService.CreateAffiliateAsync(signUpParam, new EmailSignUpParam());

            // Assert
            Assert.Equal(expectedResultInfo.Code, result.Code);
            Assert.Equal(expectedResultInfo.IsOk, result.IsOk);
            Assert.Equal(expectedResultInfo.Message, result.Message);
        }

        [Fact]
        public async Task CreateAffiliate_CreateSuccessfully_SendEmailToCsdWithCorrectParameter()
        {
            // Arrange
            const string ExpectedCsdEmail = Constant.DefaultMailContact;
            NoticeEmailParam passedParam = null;

            var signUpParam = new UserSignUpParam { IsLocal = true, Email = Constant.DefaultMailUser, UserName = Constant.DefaultUserName };
            var emailService = new Mock<IEmailService>();
            var mockApi = new Mock<ILicenseeApi>();

            emailService.Setup(e => e.SendNoticeEmailToCsd(It.IsAny<NoticeEmailParam>()))
                            .Callback<NoticeEmailParam>(p => passedParam = p);

            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                            .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.RUNNING });

            var accountService = CreateAccountService(_repository.Object, mockApi.Object, emailService.Object);

            var emailParam = CreateEmailParam();

            // Action
            await accountService.CreateAffiliateAsync(signUpParam, emailParam);

            // Assert
            emailService.Verify(e => e.SendNoticeEmailToCsd(It.IsAny<NoticeEmailParam>()), Times.Once);
            Assert.Equal(ExpectedCsdEmail, passedParam.MailContact);
            Assert.Equal(Constant.DefaultUserName, passedParam.UserName);
        }

        [Fact]
        public void GetGeneralAffInfo_WhenCallWithValidParam_ReturnCorrectData()
        {
            //Arrange
            var affId = 1;
            _repository.Setup(_ => _.Get<GeneralAffInfo>(It.IsAny<GetGeneralAffInfoCriteria>(), string.Empty))
                .Returns(new GeneralAffInfo { AffId = 1, AffCode = "100", IsMaxSubLevel = false });
            var service = CreateAccountService(_repository.Object);

            //Action
            var result = service.GetGeneralAffInfo(affId, string.Empty);

            //Assert
            Assert.Equal(affId, result.AffId);
            Assert.True(result.IsValidAff);
        }

        [Fact]
        public void GetGeneralAffInfo_WhenCallWithInvalid_ReturnCorrectData()
        {
            //Arrange
            var service = CreateAccountService(_repository.Object);

            //Action
            var result = service.GetGeneralAffInfo(123, string.Empty);

            //Assert
            Assert.Equal(0, result.AffId);
        }

        [Fact]
        public async Task CheckAndUnlockUserIfLockedAsync_WhenCall_ReturnCorrectResult()
        {
            // Arrange
            var mockApi = new Mock<ILicenseeApi>();
            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.RUNNING });

            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            var result = await accountService.CheckAndUnlockUserIfLocked("123abc");

            // Assert
            Assert.True(result.IsOk);
        }

        [Fact]
        public async Task CheckAndUnlockUserIfLocked_WhenCallWithLockUser_UnlockSuccessfully()
        {
            // Arrange
            var mockApi = new Mock<ILicenseeApi>();
            mockApi.Setup(a => a.UnlockUser(It.IsAny<int[]>())).Returns(Task.FromResult(new ApiResultInfo { IsSuccess = true }));
            mockApi.Setup(a => a.GetCustomerInfoAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .ReturnsAsync(new MemberSiteCustomerInfo { Status = CustomerStatus.LOCKED });

            var accountService = CreateAccountService(_repository.Object, mockApi.Object);

            // Action
            var result = await accountService.CheckAndUnlockUserIfLocked("123abc");

            // Assert
            Assert.True(result.IsOk);
        }

        private static AccountService CreateAccountService(IDynamicRepository repository, ILicenseeApi api = null, IEmailService emailService = null)
        {
            return new AccountService(repository, emailService ?? Mock.Of<IEmailService>(), api ?? Mock.Of<ILicenseeApi>(), Mock.Of<IAppSetting>());
        }

        private EmailSignUpParam CreateEmailParam()
        {
            return new EmailSignUpParam
            {
                MailContact = Constant.DefaultMailContact,
                RootDomain = Constant.DefaultRootDomain
            };
        }
    }
}