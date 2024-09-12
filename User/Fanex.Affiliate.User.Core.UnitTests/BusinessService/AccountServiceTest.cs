using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.BusinessService.Apis;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Apis;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class AccountServiceTest
    {
        [Fact]
        public async Task CheckPlayerAccountIsActived_AcountIsActived_ReturnResultCorrectly()
        {
            // Arrange
            var expectedResult = new PlayerAccountCheckingResult { IsOk = true, Message = string.Empty };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedResult);
            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            var result = await accountService.CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public async Task CheckPlayerAccountIsActived_AcountIsNotActived_CallGetDepositPlayerAccountWithCorrectParam()
        {
            // Arrange
            var expectedResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedResult);

            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(new DepositCustomerInfo());
            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);
            var insertingResult = new LicenseeCustDbApiResult
            {
                Result = new CustInfo()
            };
            var licenseeCustDbApiService = new Mock<ILicenseeCustDbApiService>();
            licenseeCustDbApiService.Setup(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount)).Returns(Task.FromResult(insertingResult));

            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            await accountService.CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            accountRepository.Verify(ac => ac.GetDepositPlayerAccount(expectedResult.PlayerUserName), Times.Once);
        }

        [Fact]
        public async Task CheckPlayerAccountIsActived_IsNotActivedAndCanNotGetDepositAccount_ReturnResultCorrectly()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = new DepositCustomerInfo { Success = 1 };
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var accountService = CreateAccountService(accountRepository.Object);

            var expectedResult = new ResultInfo
            {
                IsOk = false,
                Message = Lang.errDepositDBConnectFailed
            };

            // Action
            var result = await accountService.CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public async Task CheckPlayerAccountIsActived_IsNotActivedAndGetDepositAccountSuccessfully_CallInsertPlayerAccountForIbcWithCorrectParam()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);
            
            var insertingResult = new LicenseeCustDbApiResult
            {
                Result = new CustInfo()
            };
            var licenseeCustDbApiService = new Mock<ILicenseeCustDbApiService>();
            licenseeCustDbApiService.Setup(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount)).Returns(Task.FromResult(insertingResult));

            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            await accountService .CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            licenseeCustDbApiService.Verify(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount), Times.Once);
        }

        [Fact]
        public async Task CheckPlayerAccountIsActived_IsNotActivedAndInsertPlayerAccountForIbcFailed_ReturnInsertingResultCorrectly()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new LicenseeCustDbApiResult
            {
                ErrorCode = 999,
                ErrorMessage = "Insert Failed"
            };
            var licenseeCustDbApiService = new Mock<ILicenseeCustDbApiService>();
            licenseeCustDbApiService.Setup(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount)).Returns(Task.FromResult(insertingResult));

            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            var result = await accountService.CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.False(result.IsOk);
        }

        [Fact]
        public async Task CheckPlayerAccountIsActived_IsNotActivedAndGetDepositAccountSuccessfully_CallUpdatePlayerAccountStatusForDepositWithCorrectParam()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new LicenseeCustDbApiResult
            {
                Result = new CustInfo
                {
                    CustID = 100,
                    Username = "ibcUsername"
                }
            };
            var licenseeCustDbApiService = new Mock<ILicenseeCustDbApiService>();
            licenseeCustDbApiService.Setup(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount)).Returns(Task.FromResult(insertingResult));

            DepositPlayerAccountParam passedAccountParam = null;
            var depositAccountParam = new DepositPlayerAccountParam
            {
                DepUserName = Constant.DefaultUserName,
                IbcCustId = insertingResult.Result.CustID,
                IbcUserName = insertingResult.Result.Username
            };
            accountRepository.Setup(ac => ac.UpdatePlayerAccountStatusForDeposit(It.IsAny<DepositPlayerAccountParam>()))
                                            .Callback<DepositPlayerAccountParam>(p => passedAccountParam = p);

            var accountService = CreateAccountService(accountRepository.Object);
            var expectedResult = JsonConvert.SerializeObject(depositAccountParam);

            // Action
            await accountService.CheckPlayerAccountIsActivedAsync (Constant.DefaultAffId, Constant.DefaultLanguage);
            var result = JsonConvert.SerializeObject(passedAccountParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CheckPlayerAccountIsActived_IsNotActivedAndUpdateAccountStatusSuccessfully_ReturnResultCorrectly()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new LicenseeCustDbApiResult
            {
                Result = new CustInfo
                {
                    CustID = 100,
                    Username = "ibcUsername"
                }
            };
            var licenseeCustDbApiService = new Mock<ILicenseeCustDbApiService>();
            licenseeCustDbApiService.Setup(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount)).Returns(Task.FromResult(insertingResult));

            var expectedUpdateResult = new ResultInfo();
            accountRepository.Setup(ac => ac.UpdatePlayerAccountStatusForDeposit(It.IsAny<DepositPlayerAccountParam>()))
                                            .Returns(expectedUpdateResult);

            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            var result = await accountService.CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedUpdateResult, result);
        }

        [Theory]
        [InlineData(101)]
        [InlineData(102)]
        public async Task CheckPlayerAccountIsActived_IsNotActivedAndUpdateAccountStatusFailed_ReturnErrorMessageByCodeCorrectly(int errCode)
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new LicenseeCustDbApiResult
            {
                Result = new CustInfo
                {
                    CustID = 100,
                    Username = "ibcUsername"
                }
            };
            var licenseeCustDbApiService = new Mock<ILicenseeCustDbApiService>();
            licenseeCustDbApiService.Setup(ac => ac.Insert12BetPlayerAccountToIBCAsync(depositAccount)).Returns(Task.FromResult(insertingResult));
            accountRepository.Setup(ac => ac.UpdatePlayerAccountStatusForDeposit(It.IsAny<DepositPlayerAccountParam>()))
                                            .Returns(new ResultInfo())
                                            .Callback<DepositPlayerAccountParam>(p => p.ErrorCode = errCode);

            var accountService = CreateAccountService(accountRepository.Object);

            var messageByCode = new Dictionary<int, string>
            {
                [101] = Lang.errUserNotFound,
                [102] = Lang.errExistAccount
            };
            var expectedUpdateResult = new ResultInfo { IsOk = false, Message = messageByCode[errCode] };

            // Action
            var result = await accountService.CheckPlayerAccountIsActivedAsync(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedUpdateResult.IsOk, result.IsOk);
            Assert.Equal(expectedUpdateResult.Message, result.Message);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateAffiliate_ValidDepositAccountInfo_CallRepositoryByEnvironment(bool isLocalTest)
        {
            // Arrange
            var signUpParam = new UserSignUpParam { IsLocal = isLocalTest };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(It.IsAny<string>())).Returns(new DepositCustomerInfo { Success = 0 });
            accountRepository.Setup(ac => ac.CreateAffiliate(signUpParam)).Returns(new ResultInfo());

            var accountService = CreateAccountService(accountRepository.Object);
            var times = isLocalTest ? Times.Never() : Times.Once();

            // Action
            accountService.CreateAffiliate(signUpParam, new EmailSignUpParam());

            // Assert
            accountRepository.Verify(ac => ac.CreateAffiliate(signUpParam), Times.Once);
            accountRepository.Verify(ac => ac.GetDepositPlayerAccount(It.IsAny<string>()), times);
        }

        [Fact]
        public void CreateAffiliate_InvalidPlayerDepositInfo_DoNotCallRepository()
        {
            // Arrange
            const string PlayerUserName = "player";
            var depositAccount = new DepositCustomerInfo { Success = 1 };
            var signUpParam = new UserSignUpParam { Player_UserName = PlayerUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(PlayerUserName)).Returns(depositAccount);
            accountRepository.Setup(ac => ac.CreateAffiliate(signUpParam)).Returns(new ResultInfo { IsOk = false, Message = "Failed" });

            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            accountService.CreateAffiliate(signUpParam, new EmailSignUpParam());

            // Assert
            accountRepository.Verify(ac => ac.CreateAffiliate(signUpParam), Times.Never);
        }

        [Fact]
        public void CreateAffiliate_InvalidPlayerDepositInfo_ReturnExpectedResult()
        {
            // Arrange
            var signUpParam = new UserSignUpParam { IsLocal = false };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(It.IsAny<string>())).Returns(new DepositCustomerInfo { Success = 1 });
            accountRepository.Setup(ac => ac.CreateAffiliate(signUpParam)).Returns(new ResultInfo { IsOk = false, Message = "Failed" });

            var accountService = CreateAccountService(accountRepository.Object);

            var expectedResult = new ResultInfo { IsOk = false, Message = Lang.errDepositDBConnectFailed };

            // Action
            var result = accountService.CreateAffiliate(signUpParam, new EmailSignUpParam());

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public void CreateAffiliate_CreateSuccessfully_ReturnCorrectResult()
        {
            // Arrange
            var expectedResultInfo = new ResultInfo { IsOk = true };
            var signUpParam = new UserSignUpParam { IsLocal = true };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CreateAffiliate(signUpParam)).Returns(expectedResultInfo);

            var accountService = CreateAccountService(accountRepository.Object);

            // Action
            var result = accountService.CreateAffiliate(signUpParam, new EmailSignUpParam());

            // Assert
            Assert.Equal(expectedResultInfo, result);
        }

        [Fact]
        public void CreateAffiliate_CreateSuccessfully_SendEmailToUserWithCorrectParameter()
        {
            // Arrange
            EmailBaseParam passedParam = null;

            var signUpParam = new UserSignUpParam { IsLocal = true, Email = Constant.DefaultMailUser };
            var accountRepository = GetMockAccountRepository(signUpParam: signUpParam);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendSignUpEmail(It.IsAny<EmailSignUpParam>())).Callback<EmailBaseParam>(p => passedParam = p);

            var accountService = CreateAccountService(accountRepository, emailService.Object);

            var expectedEmailParam = CreateEmailParam();
            expectedEmailParam.MailTo = signUpParam.Email;
            expectedEmailParam.User = new UserInfo
            {
                PreferredLanguage = signUpParam.PreferredLanguage,
                FirstName = signUpParam.FirstName
            };
            var expectedResult = JsonConvert.SerializeObject(expectedEmailParam);

            var emailParam = CreateEmailParam();

            // Action
            accountService.CreateAffiliate(signUpParam, emailParam);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            emailService.Verify(e => e.SendSignUpEmail(It.IsAny<EmailSignUpParam>()), Times.Once);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CreateAffiliate_CreateSuccessfully_SendEmailToCsdWithCorrectParameter()
        {
            // Arrange
            const string ExpectedCsdEmail = Constant.DefaultMailContact;
            NoticeEmailParam passedParam = null;

            var signUpParam = new UserSignUpParam { IsLocal = true, Email = Constant.DefaultMailUser };
            var accountRepository = GetMockAccountRepository(signUpParam: signUpParam);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendNoticeEmailToCsd(It.IsAny<NoticeEmailParam>()))
                            .Callback<NoticeEmailParam>(p => passedParam = p);

            var accountService = CreateAccountService(accountRepository, emailService.Object);

            var emailParam = CreateEmailParam();

            // Action
            accountService.CreateAffiliate(signUpParam, emailParam);

            // Assert
            emailService.Verify(e => e.SendNoticeEmailToCsd(It.IsAny<NoticeEmailParam>()), Times.Once);
            Assert.Equal(ExpectedCsdEmail, passedParam.MailTo);
            Assert.Equal(Constant.DefaultUserName, passedParam.UserName);
        }

        private static AccountService CreateAccountService(
            IAccountRepository repository,
            IEmailService emailService = null,
            ILicenseeCustDbApiService licenseeCustDbApiService = null)
        {
            return new AccountService(repository, emailService ?? Mock.Of<IEmailService>(), licenseeCustDbApiService ?? Mock.Of<ILicenseeCustDbApiService>());
        }

        private DepositCustomerInfo GetDepositAccount()
        {
            return new DepositCustomerInfo
            {
                Success = 0,
                AffiliateId = Constant.DefaultAffId,
                AgentId = 1,
                Country = "Brazil",
                DepositUserId = 2,
                DepositUserName = Constant.DefaultUserName,
                OddsType = "OddsType",
                Password = "1234aa"
            };
        }

        private IAccountRepository GetMockAccountRepository(UserSignUpParam signUpParam = null)
        {
            var accountRepository = new Mock<IAccountRepository>();
            signUpParam = signUpParam ?? new UserSignUpParam { IsLocal = true };
            accountRepository.Setup(ac => ac.CreateAffiliate(signUpParam))
                    .Returns(new ResultInfo { IsOk = true })
                    .Callback<UserSignUpParam>(p =>
                    {
                        p.UserName = Constant.DefaultUserName;
                    });

            return accountRepository.Object;
        }

        private EmailSignUpParam CreateEmailParam()
        {
            return new EmailSignUpParam
            {
                MailContact = Constant.DefaultMailContact,
                RootDomain = Constant.DefaultRootDomain,
            };
        }
    }
}