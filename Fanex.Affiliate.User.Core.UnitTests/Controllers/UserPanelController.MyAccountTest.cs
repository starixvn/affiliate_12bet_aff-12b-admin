using System.Web.Mvc;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class MyAccountTest
    {
        private const string PlayerUserName = "player";

        [Fact]
        public void GetMyAccount_WhenCall_ReturnCorrectModel()
        {
            // Arrange
            var expectedAccount = new AccountInformationViewModel { PreferredLanguage = Constant.DefaultLanguage };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetAccountInfo(Constant.DefaultAffId)).Returns(expectedAccount);
            var controller = MockingHelper.GetUserPanelController(accountRepository: accountRepository.Object);

            // Action
            var actionResult = controller.MyAccount() as ViewResult;
            var result = actionResult.Model as AccountInformationViewModel;

            // Assert
            Assert.Equal(expectedAccount, result);
            Assert.False(expectedAccount.IsRenderState);
            Assert.Equal("English", expectedAccount.LanguageName);
        }

        [Fact]
        public void GetMyAccount_ChineseLanguage_ReturnCorrectModel()
        {
            // Arrange
            const string ExpectedLanguageName = "aa-AA";
            var expectedAccount = new AccountInformationViewModel { PreferredLanguage = ExpectedLanguageName };
            var sessionService = MockingHelper.GetSessionService();
            sessionService.CurrentLanguage = "zh-TW";
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetAccountInfo(Constant.DefaultAffId)).Returns(expectedAccount);
            var controller = MockingHelper.GetUserPanelController(accountRepository: accountRepository.Object, sessionService: sessionService);

            // Action
            var actionResult = controller.MyAccount() as ViewResult;
            var result = actionResult.Model as AccountInformationViewModel;

            // Assert
            Assert.True(expectedAccount.IsRenderState);
            Assert.Equal(ExpectedLanguageName, expectedAccount.LanguageName);
        }

        [Fact]
        public void PostMyAccount_IsInternal_ReturnCorrectResult()
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = true;

            var controller = MockingHelper.GetUserPanelController(sessionService: sessionService);

            // Action
            var actionResult = controller.MyAccount(new AccountInformationViewModel()) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(Lang.ErrAuthorized, result.Message);
        }

        [Fact]
        public void PostMyAccount_DepositAccountIsNotValid_ReturnCorrectResult()
        {
            // Arrange
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetAccountInfo(Constant.DefaultAffId)).Returns(new AccountInformationViewModel { PlayerUsername = PlayerUserName });
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(PlayerUserName)).Returns(new DepositAccountInfo { Success = 1 });
            var controller = MockingHelper.GetUserPanelController(accountRepository: accountRepository.Object);

            // Action
            var actionResult = controller.MyAccount(new AccountInformationViewModel()) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(Lang.errDepositDBConnectFailed, result.Message);
        }

        [Fact]
        public void PostMyAccount_UpdateSuccessfully_ReturnCorrectResult()
        {
            // Arrange
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetAccountInfo(Constant.DefaultAffId)).Returns(new AccountInformationViewModel { PlayerUsername = PlayerUserName });
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount(PlayerUserName)).Returns(new DepositAccountInfo { Success = 0 });
            accountRepository.Setup(ac => ac.UpdateAccountInformation(It.IsAny<UpdateAccountParam>())).Returns(new ResultInfo { IsOk = true });
            var controller = MockingHelper.GetUserPanelController(accountRepository: accountRepository.Object);

            // Action
            var actionResult = controller.MyAccount(new AccountInformationViewModel()) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            Assert.True(result.IsOk);
        }

        [Fact]
        public void PostMyAccount_UpdateAccount_PasssedParamCorrectly()
        {
            // Arrange
            var param = CreateParam();
            UpdateAccountParam passedParam = null;
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetAccountInfo(Constant.DefaultAffId)).Returns(new AccountInformationViewModel());
            accountRepository.Setup(ac => ac.GetDepositPlayerAccount($"Encoded{PlayerUserName}")).Returns(new DepositAccountInfo { Success = 0 });
            accountRepository.Setup(ac => ac.UpdateAccountInformation(It.IsAny<UpdateAccountParam>()))
                .Returns(new ResultInfo { IsOk = true })
                .Callback<UpdateAccountParam>(p => passedParam = p);
            var controller = MockingHelper.GetUserPanelController(accountRepository: accountRepository.Object);

            var expectedResult = JsonConvert.SerializeObject(CreateExpectedParam());

            // Action
            var actionResult = controller.MyAccount(param);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        private AccountInformationViewModel CreateParam()
        {
            return new AccountInformationViewModel
            {
                Phone = "Phone",
                MobilePhone = "MobilePhone",
                SiteUrl = "SiteUrl",
                MoreSiteUrls = "MoreSiteUrls",
                NoSiteDesc = "NoSiteDesc",
                PlayerUsername = PlayerUserName
            };
        }

        private UpdateAccountParam CreateExpectedParam()
        {
            return new UpdateAccountParam
            {
                Phone = "EncodedPhone",
                MobilePhone = "EncodedMobilePhone",
                SiteUrl = "EncodedSiteUrl",
                MoreSiteUrls = "EncodedMoreSiteUrls",
                NoSiteDesc = "EncodedNoSiteDesc",
                Player_UserName = $"Encoded{PlayerUserName}",
                AffId = Constant.DefaultAffId,
                Ip = "::1",
                Language = Constant.DefaultLanguage
            };
        }
    }
}