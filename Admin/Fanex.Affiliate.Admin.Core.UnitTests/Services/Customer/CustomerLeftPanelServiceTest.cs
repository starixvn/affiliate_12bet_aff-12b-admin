using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Customer;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class CustomerLeftPanelServiceTest
    {
        private ILifetimeScopeProvider _scopeProvider;

        [Fact]
        public void GetAffiliateInfoOnLeftPanel_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.GetAffiliateInfoOnLeftPanel(It.IsAny<string>()));
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object);

            // Action
            var result = service.GetAffiliateInfoOnLeftPanel("12BETAFF226", "", "");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAffiliateInfoOnLeftPanel_WhenCall_ReturnCorrectDataWithAsian()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.InternalAsianAffiliateUrl).Returns("http://localhost:55690/Auth/AdminLogin");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.GetAffiliateInfoOnLeftPanel(It.IsAny<string>())).Returns(new AffiliateInfoOnLeftPanel { AffName = "12BETAFF226", AffType = 2 });
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.GetAffiliateInfoOnLeftPanel("12BETAFF226", "123", "234");

            // Assert
            Assert.Equal("12BETAFF226", result.AffName);
            Assert.Equal(AffType.Asian, result.AffType);
        }

        [Fact]
        public void GetAffiliateInfoOnLeftPanel_WhenCall_ReturnCorrectDataWithEuropean()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.InternalEuropeanAffiliateUrl).Returns("http://localhost:55690/Auth/AdminLogin");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);


            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.GetAffiliateInfoOnLeftPanel(It.IsAny<string>())).Returns(new AffiliateInfoOnLeftPanel { AffName = "12BETAFF226", AffType = 4 });
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.GetAffiliateInfoOnLeftPanel("12BETAFF226", "123", "234");

            // Assert
            Assert.Equal("12BETAFF226", result.AffName);
            Assert.Equal(AffType.European, result.AffType);
        }

        [Fact]
        public void GetOnlineList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = "12BETAFF226";
            var passedParam = string.Empty;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(r => r.GetAffiliateInfoOnLeftPanel(It.IsAny<string>())).Returns(new AffiliateInfoOnLeftPanel());
            repository.Setup(comm => comm.GetOnlineList(param)).Returns(new List<OnlineList> { new OnlineList() }).Callback<string>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.GetOnlineList(param);

            // Assert
            Assert.Single(result);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetOnlineList_WhenCallWithInvalidParam_ReturnEmptyData()
        {
            // Arrange
            var param = "12BETAFF226";
            var passedParam = string.Empty;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.GetOnlineList(param)).Returns(new List<OnlineList> { new OnlineList() }).Callback<string>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.GetOnlineList(param);

            // Assert
            Assert.Empty(result);
            Assert.NotEqual(param, passedParam);
        }

        [Fact]
        public void UpdateAffType_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new UpdateAffTypeParam();
            UpdateAffTypeParam passedParam = null;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateAffType(param)).Returns(new ResultInfo()).Callback<UpdateAffTypeParam>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.UpdateAffType(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateAffType_WhenCallWithNotHasPermission_ReturnCorrectData()
        {
            // Arrange
            var param = new UpdateAffTypeParam();
            UpdateAffTypeParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateAffType(param)).Returns(new ResultInfo()).Callback<UpdateAffTypeParam>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object);

            // Action
            var result = service.UpdateAffType(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.NotEqual(param, passedParam);
        }

        [Fact]
        public void UpdateClosedStatus_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new UpdateClosedStatusParam();
            UpdateClosedStatusParam passedParam = null;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateClosedStatus(param)).Returns(new ResultInfo()).Callback<UpdateClosedStatusParam>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.UpdateClosedStatus(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateClosedStatusNotHasPermission_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new UpdateClosedStatusParam();
            UpdateClosedStatusParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateClosedStatus(param)).Returns(new ResultInfo()).Callback<UpdateClosedStatusParam>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object);

            // Action
            var result = service.UpdateClosedStatus(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.NotEqual(param, passedParam);
        }

        [Fact]
        public void UpdateLanguage_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new UpdateLanguageParam();
            UpdateLanguageParam passedParam = null;
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateLanguage(param)).Returns(new ResultInfo()).Callback<UpdateLanguageParam>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.UpdateLanguage(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateLanguage_WhenCallNotHasPermission_ReturnCorrectData()
        {
            // Arrange
            var param = new UpdateLanguageParam();
            UpdateLanguageParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateLanguage(param)).Returns(new ResultInfo()).Callback<UpdateLanguageParam>(p => passedParam = p);
            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object);

            // Action
            var result = service.UpdateLanguage(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.NotEqual(param, passedParam);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithNotHasPermission_ReturnError()
        {
            // Arrange
            var param = new UpdateStatusParam { AffId = 1271808, Status = "disapproved" };
            UpdateStatusParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateStatus(param)).Returns(new ResultInfo { Code = -999 }).Callback<UpdateStatusParam>(p => passedParam = p);

            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object);

            // Action
            var result = service.UpdateStatus(param, new AffInformationEmail());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.NotEqual(param, passedParam);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithDisapprovedEmail_ReturnCorrectDataWithDefaultLanguage()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("https://ha.nexdev.net:7613/12BetUserAsianNew");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this._scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var param = new UpdateStatusParam { AffId = 1271808, Status = "disapproved" };
            UpdateStatusParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateStatus(param)).Returns(new ResultInfo()).Callback<UpdateStatusParam>(p => passedParam = p);
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(aff => aff.GetAffiliateBasicInfo(It.IsAny<int>(), null)).Returns(new AffiliateBasicInfo { AffId = 1271808, AffType = 2 });
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var service = CreateCustomerLeftPanelService(affiliateService: affiliateService.Object, customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.UpdateStatus(param, new AffInformationEmail());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithDisapprovedEmail_ReturnCorrectData()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("https://ha.nexdev.net:7613/12BetUserAsianNew");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var param = new UpdateStatusParam { AffId = 1271808, Status = "disapproved" };
            UpdateStatusParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateStatus(param)).Returns(new ResultInfo()).Callback<UpdateStatusParam>(p => passedParam = p);
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(aff => aff.GetAffiliateBasicInfo(It.IsAny<int>(), null)).Returns(new AffiliateBasicInfo { AffId = 1271808, AffType = 2, PreferredLanguage = "vi-VN" });
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var service = CreateCustomerLeftPanelService(affiliateService: affiliateService.Object, customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.UpdateStatus(param, new AffInformationEmail());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateStatus_WhenCallWithDisapprovedEmail_ReturnError()
        {
            // Arrange
            var param = new UpdateStatusParam { AffId = 1271808, Status = "disapproved" };
            UpdateStatusParam passedParam = null;
            var repository = new Mock<ICustomerLeftPanelRepository>();
            repository.Setup(comm => comm.UpdateStatus(param)).Returns(new ResultInfo { Code = -999 }).Callback<UpdateStatusParam>(p => passedParam = p);
            var authService = new Mock<IAuthService>();
            authService.Setup(a => a.IsUserHasPermissionOnAff(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var service = CreateCustomerLeftPanelService(customerLeftPanelRepository: repository.Object, authService: authService.Object);

            // Action
            var result = service.UpdateStatus(param, new AffInformationEmail());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        private static CustomerLeftPanelService CreateCustomerLeftPanelService(
            IEmailService emailService = null,
            IAffiliateService affiliateService = null,
            IAuthService authService = null,
            ISessionService sessionService = null,
            ICustomerLeftPanelRepository customerLeftPanelRepository = null)
        {
            var fakeEmailService = emailService ?? Mock.Of<IEmailService>();
            var fakeAffiliateService = affiliateService ?? Mock.Of<IAffiliateService>();
            var fakeAuthService = authService ?? Mock.Of<IAuthService>();
            var fakeSessionService = sessionService ?? Mock.Of<ISessionService>();
            var fakeCustomerLeftPanelRepository = customerLeftPanelRepository ?? Mock.Of<ICustomerLeftPanelRepository>();

            return new CustomerLeftPanelService(fakeEmailService, fakeAffiliateService, fakeAuthService, fakeSessionService, fakeCustomerLeftPanelRepository);
        }
    }
}
