using System.Collections.Generic;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class AccountServiceTest
    {
        [Fact]
        public void CheckPlayerAccountIsActived_AcountIsActived_ReturnResultCorrectly()
        {
            // Arrange
            var expectedResult = new PlayerAccountCheckingResult { IsOk = true, Message = string.Empty };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedResult);
            var accountService = new AccountService(accountRepository.Object);

            // Action
            var result = accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public void CheckPlayerAccountIsActived_AcountIsNotActived_CallGetDepositPlayerAccountWithCorrectParam()
        {
            // Arrange
            var expectedResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedResult);

            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(new DepositAccountInfo());
            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);
            accountRepository.Setup(ac => ac.InsertPlayerAccountForIbc(depositAccount)).Returns(new IbcPlayerAccountInsertResult());

            var accountService = new AccountService(accountRepository.Object);

            // Action
            accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            accountRepository.Verify(ac => ac.GetDepositPlayerAccount(expectedResult.PlayerUserName), Times.Once);
        }

        [Fact]
        public void CheckPlayerAccountIsActived_IsNotActivedAndCanNotGetDepositAccount_ReturnResultCorrectly()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = new DepositAccountInfo { Success = 1 };
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var accountService = new AccountService(accountRepository.Object);

            var expectedResult = new ResultInfo
            {
                IsOk = false,
                Message = Lang.errDepositDBConnectFailed
            };

            // Action
            var result = accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public void CheckPlayerAccountIsActived_IsNotActivedAndGetDepositAccountSuccessfully_CallInsertPlayerAccountForIbcWithCorrectParam()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);
            accountRepository.Setup(ac => ac.InsertPlayerAccountForIbc(depositAccount)).Returns(new IbcPlayerAccountInsertResult());

            var accountService = new AccountService(accountRepository.Object);

            // Action
            accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            accountRepository.Verify(ac => ac.InsertPlayerAccountForIbc(depositAccount), Times.Once);
        }

        [Fact]
        public void CheckPlayerAccountIsActived_IsNotActivedAndInsertPlayerAccountForIbcFailed_ReturnInsertingResultCorrectly()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new IbcPlayerAccountInsertResult { IsOk = false, Message = "Insert Failed" };
            accountRepository.Setup(ac => ac.InsertPlayerAccountForIbc(depositAccount)).Returns(insertingResult);

            var accountService = new AccountService(accountRepository.Object);

            // Action
            var result = accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(insertingResult, result);
        }

        [Fact]
        public void CheckPlayerAccountIsActived_IsNotActivedAndGetDepositAccountSuccessfully_CallUpdatePlayerAccountStatusForDepositWithCorrectParam()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new IbcPlayerAccountInsertResult
            {
                IsOk = true,
                Message = string.Empty,
                CustId = 100,
                UserName = "ibcUsername"
            };
            accountRepository.Setup(ac => ac.InsertPlayerAccountForIbc(depositAccount)).Returns(insertingResult);

            DepositPlayerAccountParam passedAccountParam = null;
            var depositAccountParam = new DepositPlayerAccountParam
            {
                DepUserName = Constant.DefaultUserName,
                IbcCustId = insertingResult.CustId,
                IbcUserName = insertingResult.UserName
            };
            accountRepository.Setup(ac => ac.UpdatePlayerAccountStatusForDeposit(It.IsAny<DepositPlayerAccountParam>()))
                                            .Callback<DepositPlayerAccountParam>(p => passedAccountParam = p);

            var accountService = new AccountService(accountRepository.Object);
            var expectedResult = JsonConvert.SerializeObject(depositAccountParam);

            // Action
            accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);
            var result = JsonConvert.SerializeObject(passedAccountParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CheckPlayerAccountIsActived_IsNotActivedAndUpdateAccountStatusSuccessfully_ReturnResultCorrectly()
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new IbcPlayerAccountInsertResult
            {
                IsOk = true,
                Message = string.Empty,
                CustId = 100,
                UserName = "ibcUsername"
            };
            accountRepository.Setup(ac => ac.InsertPlayerAccountForIbc(depositAccount)).Returns(insertingResult);

            var expectedUpdateResult = new ResultInfo();
            accountRepository.Setup(ac => ac.UpdatePlayerAccountStatusForDeposit(It.IsAny<DepositPlayerAccountParam>()))
                                            .Returns(expectedUpdateResult);

            var accountService = new AccountService(accountRepository.Object);

            // Action
            var result = accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedUpdateResult, result);
        }

        [Theory]
        [InlineData(101)]
        [InlineData(102)]
        public void CheckPlayerAccountIsActived_IsNotActivedAndUpdateAccountStatusFailed_ReturnErrorMessageByCodeCorrectly(int errCode)
        {
            // Arrange
            var expectedCheckingResult = new PlayerAccountCheckingResult { IsOk = false, PlayerUserName = Constant.DefaultUserName };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                            .Returns(expectedCheckingResult);

            var depositAccount = GetDepositAccount();
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(Constant.DefaultUserName)).Returns(depositAccount);

            var insertingResult = new IbcPlayerAccountInsertResult
            {
                IsOk = true,
                Message = string.Empty,
                CustId = 100,
                UserName = "ibcUsername"
            };
            accountRepository.Setup(ac => ac.InsertPlayerAccountForIbc(depositAccount)).Returns(insertingResult);
            accountRepository.Setup(ac => ac.UpdatePlayerAccountStatusForDeposit(It.IsAny<DepositPlayerAccountParam>()))
                                            .Returns(new ResultInfo())
                                            .Callback<DepositPlayerAccountParam>(p => p.ErrorCode = errCode);

            var accountService = new AccountService(accountRepository.Object);

            var messageByCode = new Dictionary<int, string>
            {
                [101] = Lang.errUserNotFound,
                [102] = Lang.errExistAccount
            };
            var expectedUpdateResult = new ResultInfo { IsOk = false, Message = messageByCode[errCode] };

            // Action
            var result = accountService.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage);

            // Assert
            Assert.Equal(expectedUpdateResult.IsOk, result.IsOk);
            Assert.Equal(expectedUpdateResult.Message, result.Message);
        }

        private DepositAccountInfo GetDepositAccount()
        {
            return new DepositAccountInfo
            {
                Success = 0,
                AffiliateId = Constant.DefaultAffId,
                AgentId = 1,
                BonusCode = "Code",
                Country = "Brazil",
                DepositUserId = 2,
                DepositUserName = Constant.DefaultUserName,
                Email = Constant.DefaultMailUser,
                FirstName = "test",
                LastName = "test",
                OddsType = "OddsType",
                Password = "1234aa"
            };
        }
    }
}