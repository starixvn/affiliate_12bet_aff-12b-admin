using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.PaymentRequest;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Api;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.Email;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Payment
{
    public class PaymentRequestServiceTest
    {
        private ILifetimeScopeProvider scopeProvider;

        [Fact]
        public void GetPaymentRequestList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PaymentRequestParam();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<PaymentRequest>(It.IsAny<PaymentRequestCriteria>())).Returns(new List<PaymentRequest> { new PaymentRequest() });

            var service = CreatePaymentRequestService(dynamicRepository: dynamicRepository.Object);

            // Action
            var result = service.GetPaymentRequestList(param);

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetPaymentRequestDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var transId = 123;
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { TransId = transId });

            var service = CreatePaymentRequestService(dynamicRepository: dynamicRepository.Object);

            // Action
            var result = service.GetPaymentRequestDetail(transId);

            // Assert
            Assert.Equal(transId, result.TransId);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_UpdateFailed()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "abc" });

            var service = CreatePaymentRequestService(dynamicRepository: dynamicRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentRequestUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Payment request is not valid", result.Message);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_UpdateFailedWithNull()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });

            var service = CreatePaymentRequestService(dynamicRepository: dynamicRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentRequestUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_UpdateSuccessful()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("Asian");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>()));
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { AffType = 2 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentRequestUpdateParam(), new PaymentRequestUpdateEmail());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_UpdateSuccessfulHasPreferredLanguage()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>()));
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentRequestUpdateParam(), new PaymentRequestUpdateEmail());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task HandlePaymentRequest_WhenCallWithInvalidCustomer_ReturnError()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.MemberSiteId).Returns(1);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var licenseeService = new Mock<ILicenseeService>();
            licenseeService.Setup(a => a.GetMemberSiteCustomerInfo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new MemberSiteCustomerInfo { Status = 4 }));
            licenseeService.Setup(a => a.CheckCustomerIsValid(It.IsAny<MemberSiteCustomerInfo>())).Returns(new ResultInfo { Code = 4 });
            licenseeService.Setup(a => a.UnlockUser(It.IsAny<int>())).Returns(Task.FromResult(new ResultInfo { Code = ErrCode.GeneralErrorCode }));

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object, licenseeService.Object, appSetting.Object);

            // Action
            var result = await service.HandlePaymentRequest(new PaymentRequestUpdateParam(), new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public async Task HandlePaymentRequest_WhenCall_UpdateFailed()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.MemberSiteId).Returns(1);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>())).Callback<UpdatePaymentRequestCommand>(u => u.ErrCode = ErrCode.GeneralErrorCode);

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var licenseeService = new Mock<ILicenseeService>();
            licenseeService.Setup(a => a.GetMemberSiteCustomerInfo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new MemberSiteCustomerInfo()));
            licenseeService.Setup(a => a.CheckCustomerIsValid(It.IsAny<MemberSiteCustomerInfo>())).Returns(new ResultInfo { Code = ErrCode.GeneralErrorCode });

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object, licenseeService.Object, appSetting.Object);

            // Action
            var result = await service.HandlePaymentRequest(new PaymentRequestUpdateParam(), new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public async Task HandlePaymentRequest_WhenCallWithReject_UpdateSuccessfully()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.MemberSiteId).Returns(1);
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>()));

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var licenseeService = new Mock<ILicenseeService>();
            licenseeService.Setup(a => a.GetMemberSiteCustomerInfo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new MemberSiteCustomerInfo()));
            licenseeService.Setup(a => a.CheckCustomerIsValid(It.IsAny<MemberSiteCustomerInfo>())).Returns(new ResultInfo());

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object, licenseeService.Object, appSetting.Object);

            // Action
            var result = await service.HandlePaymentRequest(new PaymentRequestUpdateParam(), new PaymentRequestUpdateEmail());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task HandlePaymentRequest_WhenCallWithApproved_UpdateSuccessfully()
        {
            // Arrange
            MockingHelper.SetUpLogEngineEnviroment();
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.MemberSiteId).Returns(1);
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>()));

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var licenseeService = new Mock<ILicenseeService>();
            licenseeService.Setup(a => a.GetMemberSiteCustomerInfo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new MemberSiteCustomerInfo()));
            licenseeService.Setup(a => a.CheckCustomerIsValid(It.IsAny<MemberSiteCustomerInfo>())).Returns(new ResultInfo());
            licenseeService.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentRequestInfo>())).Returns(Task.FromResult(new ResultInfo()));

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object, licenseeService.Object, appSetting.Object);

            // Action
            var result = await service.HandlePaymentRequest(new PaymentRequestUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task HandlePaymentRequest_WhenCallWithApproved_UpdateFailed()
        {
            // Arrange
            MockingHelper.SetUpLogEngineEnviroment();
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.MemberSiteId).Returns(1);
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>()));

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var licenseeService = new Mock<ILicenseeService>();
            licenseeService.Setup(a => a.GetMemberSiteCustomerInfo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new MemberSiteCustomerInfo()));
            licenseeService.Setup(a => a.CheckCustomerIsValid(It.IsAny<MemberSiteCustomerInfo>())).Returns(new ResultInfo());
            licenseeService.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentRequestInfo>())).Returns(Task.FromResult(new ResultInfo { Code = ErrCode.GeneralErrorCode }));

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object, licenseeService.Object, appSetting.Object);

            // Action
            var result = await service.HandlePaymentRequest(new PaymentRequestUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void HandlePaymentRequest_WhenCallWithApproved_HaveException()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.MemberSiteId).Returns(1);
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<PaymentRequestDetail>(It.IsAny<PaymentRequestDetailCriteria>())).Returns(new PaymentRequestDetail { Status = "processing" });
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdatePaymentRequestCommand>()));

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var licenseeService = new Mock<ILicenseeService>();
            licenseeService.Setup(a => a.GetMemberSiteCustomerInfo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new MemberSiteCustomerInfo()));
            licenseeService.Setup(a => a.CheckCustomerIsValid(It.IsAny<MemberSiteCustomerInfo>())).Returns(new ResultInfo());
            licenseeService.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentRequestInfo>())).Returns(Task.FromResult(new ResultInfo { Code = ErrCode.GeneralErrorCode })).Callback<PaymentRequestInfo>(p => throw new System.Exception());

            var service = CreatePaymentRequestService(null, affiliateService.Object, dynamicRepository.Object, licenseeService.Object, appSetting.Object);

            // Action
            var result = service.HandlePaymentRequest(new PaymentRequestUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.Single(result.Exception.InnerExceptions);
        }

        private static PaymentService CreatePaymentRequestService(
            IEmailService emailService = null,
            IAffiliateService affiliateService = null,
            IDynamicRepository dynamicRepository = null,
            ILicenseeService licenseeService = null,
            IAppSetting appSetting = null)
        {
            var fakeEmailService = emailService ?? Mock.Of<IEmailService>();
            var fakeAffiliateService = affiliateService ?? Mock.Of<IAffiliateService>();
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();
            var fakeLicenseeService = licenseeService ?? Mock.Of<ILicenseeService>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            return new PaymentService(fakedynamicRepository, fakeAffiliateService, fakeLicenseeService, fakeEmailService, fakeAppSetting);
        }
    }
}