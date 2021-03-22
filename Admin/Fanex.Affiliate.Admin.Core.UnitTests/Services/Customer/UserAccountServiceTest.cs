using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class UserAccountServiceTest
    {
        [Fact]
        public void GetUserAccountInfo_WhenCall_ReturnNull()
        {
            // Arrange
            var param = 1271808;
            var passedParam = 0;
            var repository = new Mock<IUserAccountRepository>();
            repository.Setup(comm => comm.GetUserAccountInfo(param)).Returns(new UserAccount { AffId = 1271808 }).Callback<int>(p => passedParam = p);
            var service = CreateUserAccountService(repository.Object);

            // Action
            var result = service.GetUserAccountInfo(param);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetUserAccountInfo_WhenCallWithIsUserHasPermissionOnAff_ReturnCorrectData()
        {
            // Arrange
            var param = 1271808;
            var passedParam = 0;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var repository = new Mock<IUserAccountRepository>();
            repository.Setup(comm => comm.GetUserAccountInfo(param)).Returns(new UserAccount { AffId = 1271808 }).Callback<int>(p => passedParam = p);
            var service = CreateUserAccountService(repository.Object, authService.Object);

            // Action
            var result = service.GetUserAccountInfo(param);

            // Assert
            Assert.Equal(param, result.AffId);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateUserAccount_WhenCallWithValid_ExcuteSuccessfully()
        {
            // Arrange
            var param = GetUserAccountParam();
            UserAccountParam passedParam = null;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserClaim).Returns(new ClaimsPrincipal(new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> { new Claim(Function.AffPaymentInfo, "Write") }) }));

            var repository = new Mock<IUserAccountRepository>();
            repository.Setup(comm => comm.UpdateUserAccount(param)).Returns(new ResultInfo()).Callback<UserAccountParam>(p => passedParam = p);
            var service = CreateUserAccountService(repository.Object, authService.Object, sessionService.Object);

            // Action
            var result = service.UpdateUserAccount(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateUserAccount_WhenCallWithInvalid_ReturnError()
        {
            // Arrange
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.UserClaim).Returns(new ClaimsPrincipal(new List<ClaimsIdentity> { new ClaimsIdentity(new List<Claim> { new Claim(Function.AffPaymentInfo, "Write") }) }));
            var service = CreateUserAccountService(sessionService: sessionService.Object);

            // Action
            var result = service.UpdateUserAccount(new UserAccountParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Please describe how you would like to promote 12Bet if you don't have the URL is required", result.Message);
        }

        [Fact]
        public void GetSecurityQuestions_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IUserAccountRepository>();
            repository.Setup(comm => comm.GetSecurityQuestions("vi-VN", false)).Returns(new List<SecurityQuestion> { new SecurityQuestion() });
            var service = CreateUserAccountService(repository.Object);

            // Action
            var result = service.GetSecurityQuestions("vi-VN");

            // Assert
            Assert.Single(result);
        }

        private static UserAccountService CreateUserAccountService(
            IUserAccountRepository userAccountRepository = null,
            IAuthService authService = null,
            ISessionService sessionService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeAuthService = authService ?? Mock.Of<IAuthService>();
            var fakeUserAccountRepository = userAccountRepository ?? Mock.Of<IUserAccountRepository>();

            return new UserAccountService(fakeUserAccountRepository, fakeAuthService, fakeSessionService);
        }

        private static UserAccountParam GetUserAccountParam()
        {
            return new UserAccountParam
            {
                AffId = 1271808,
                FirstName = "Hinh",
                LastName = "Lê Văn",
                Gender = false,
                Email = "abc@gmail.com",
                Phone = "01234567899",
                ContactApps = "[{'Name':'MSN / YM / QQ','Value':'@123 Lê Văn','Order':0}]",
                Address = "Test",
                City = "test",
                State = "700000",
                Postcode = "700000",
                Country = "Viet Nam",
                MoreSiteUrls = "www.sportsbookie.com",
                SiteUrl = "https://ha.nexdev.net:7613/12BetUserAsianNew/UserPanel/Main/#MyAccount",
                PreferredPayment = "Wire",
                PlayerUsername = "bettingexp",
                NoSiteDescription = "test",
                NetellerAccountId = "1",
                MoneybookersEmail = "abc@gmail.com",
                BankName = "Test",
                BankAddress = "test",
                BankCode = "test",
                BankAccountName = "test",
                BankAccountNumber = "test",
                MailOption = false,
                ContactAppList = new List<ContactApp> { new ContactApp { Name = "MSN/YM/QQ", Value = "@123 Lê Văn", Order = 0 } }
            };
        }
    }
}
