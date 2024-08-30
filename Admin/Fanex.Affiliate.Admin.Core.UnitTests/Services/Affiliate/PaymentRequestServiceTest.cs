using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class PaymentRequestServiceTest
    {
        private ILifetimeScopeProvider _scopeProvider;

        [Fact]
        public void GetPaymentRequestList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PaymentRequestParam();
            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestList(param)).Returns(new EntityResult<PaymentRequest> { MainData = new List<PaymentRequest> { new PaymentRequest() }, TotalItem = 1 });

            var service = CreatePaymentRequestService(paymentRequestRepository: paymentRequestRepository.Object);

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
            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(transId)).Returns(new PaymentRequestDetail { TransId = transId });

            var service = CreatePaymentRequestService(paymentRequestRepository: paymentRequestRepository.Object);

            // Action
            var result = service.GetPaymentRequestDetail(transId);

            // Assert
            Assert.Equal(transId, result.TransId);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCallWithTransIdIncorrect_ReturnNull()
        {
            // Arrange
            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail());

            var service = CreatePaymentRequestService(paymentRequestRepository: paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam(), new PaymentRequestUpdateEmail());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_UpdateFailed()
        {
            // Arrange
            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult { Code = 1 });

            var service = CreatePaymentRequestService(paymentRequestRepository: paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Wrong discount", result.Message);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_UpdateFailedInvalidCode()
        {
            // Arrange
            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult { Code = -999 });

            var service = CreatePaymentRequestService(paymentRequestRepository: paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Updating the Payment Request has failed!", result.Message);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCallRejectPayment_UpdateSuccessful()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("Asian");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult());
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { AffType = 2 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = false }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData("")]
        [InlineData("<test></test>")]
        [InlineData("<root></root>")]
        public void UpdatePaymentRequest_WhenCallApprovePayment_WithInvalidDetailXml_ReturnError(string detailXml)
        {
            // Arrange
            MockingHelper.SetUpEnviroment();
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult { DetailXml = detailXml });
            paymentRequestRepository.Setup(l => l.RollbackApprovePaymentRequest(It.IsAny<ResultInfo>(), It.IsAny<string>())).Returns(new ResultInfo { Code = string.IsNullOrEmpty(detailXml) ? 0 : Code.GeneralErrorCode });
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { PreferredLanguage = "vi-VN", AffType = 4 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCallApprovePayment_HasExceptionInApprovalProcessing_ReturnError()
        {
            // Arrange
            MockingHelper.SetUpEnviroment();
            var detailXml = "<root><row NewStatus=\"approved\" NewCompletedDate=\"2023-06-29T02:08:01.940\" TransID=\"1262288293\" AffID=\"1243062\" RefID=\"100085116078\" AffUserName=\"12BETAFF030\" Player_IBCCustID=\"90708970\" Player_Currency=\"13\" Ballance_CustID=\"1233037\" AffPayout=\"-684.3222\" UserID=\"10000002\" IP=\"::1\" OldStatus=\"pending\" OldCompletedDate=\"2023-06-29T02:07:14.107\" OldTypeId=\"1\" /><row NewStatus=\"approved\" NewCompletedDate=\"2023-06-29T02:08:01.940\" TransID=\"1262288293\" AffID=\"1243062\" RefID=\"100085116078\" AffUserName=\"12BETAFF030\" Player_IBCCustID=\"90708970\" Player_Currency=\"13\" Ballance_CustID=\"1233037\" AffPayout=\"-684.3222\" UserID=\"10000002\" IP=\"::1\" OldStatus=\"pending\" OldCompletedDate=\"2023-06-29T02:07:14.107\" OldCreator=\"1243062\" OldTypeId=\"2\" /></root>";
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult { DetailXml = detailXml });
            paymentRequestRepository.Setup(l => l.RollbackApprovePaymentRequest(It.IsAny<ResultInfo>(), It.IsAny<string>())).Returns(new ResultInfo());
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { AffType = 2 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Theory]
        [InlineData(123, "Error Message")]
        [InlineData(1004, "Error Message")]
        [InlineData(234, "")]
        public void UpdatePaymentRequest_WhenCallApprovePayment_ApproveFailed_ReturnError(int errCode, string errMessage)
        {
            // Arrange
            var detailXml = "<root><row NewStatus=\"approved\" NewCompletedDate=\"2023-06-29T02:08:01.940\" TransID=\"1262288293\" AffID=\"1243062\" RefID=\"100085116078\" AffUserName=\"12BETAFF030\" Player_IBCCustID=\"90708970\" Player_Currency=\"13\" Ballance_CustID=\"1233037\" AffPayout=\"-684.3222\" UserID=\"10000002\" IP=\"::1\" OldStatus=\"pending\" OldCompletedDate=\"2023-06-29T02:07:14.107\" OldTypeId=\"1\" /><row NewStatus=\"approved\" NewCompletedDate=\"2023-06-29T02:08:01.940\" TransID=\"1262288293\" AffID=\"1243062\" RefID=\"100085116078\" AffUserName=\"12BETAFF030\" Player_IBCCustID=\"90708970\" Player_Currency=\"13\" Ballance_CustID=\"1233037\" AffPayout=\"-684.3222\" UserID=\"10000002\" IP=\"::1\" OldStatus=\"pending\" OldCompletedDate=\"2023-06-29T02:07:14.107\" OldCreator=\"1243062\" OldTypeId=\"2\" /></root>";
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult { DetailXml = detailXml });
            paymentRequestRepository.Setup(l => l.RollbackApprovePaymentRequest(It.IsAny<ResultInfo>(), It.IsAny<string>())).Returns(new ResultInfo());
            paymentRequestRepository.Setup(l => l.GetSiteDBConfigInfoByCustId(It.IsAny<string>())).Returns(new SiteDBConfigInfo { LinkedServerName = "DBTXN" });
            paymentRequestRepository.Setup(l => l.ApprovePaymentRequest(It.IsAny<PaymentApproveParam>())).Returns(new ResultInfo { Code = errCode, Message = errMessage });
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { AffType = 2 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCallApprovePayment_ReturnSuccess()
        {
            // Arrange
            var detailXml = "<root><row NewStatus=\"approved\" NewCompletedDate=\"2023-06-29T02:08:01.940\" TransID=\"1262288293\" AffID=\"1243062\" RefID=\"100085116078\" AffUserName=\"12BETAFF030\" Player_IBCCustID=\"90708970\" Player_Currency=\"13\" Ballance_CustID=\"1233037\" AffPayout=\"-684.3222\" UserID=\"10000002\" IP=\"::1\" OldStatus=\"pending\" OldCompletedDate=\"2023-06-29T02:07:14.107\" OldTypeId=\"1\" /><row NewStatus=\"approved\" NewCompletedDate=\"2023-06-29T02:08:01.940\" TransID=\"1262288293\" AffID=\"1243062\" RefID=\"100085116078\" AffUserName=\"12BETAFF030\" Player_IBCCustID=\"90708970\" Player_Currency=\"13\" Ballance_CustID=\"1233037\" AffPayout=\"-684.3222\" UserID=\"10000002\" IP=\"::1\" OldStatus=\"pending\" OldCompletedDate=\"2023-06-29T02:07:14.107\" OldCreator=\"1243062\" OldTypeId=\"2\" /></root>";
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("Euro");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var paymentRequestRepository = new Mock<IPaymentRequestRepository>();
            paymentRequestRepository.Setup(l => l.GetPaymentRequestDetail(It.IsAny<long>())).Returns(new PaymentRequestDetail { Status = "pending" });
            paymentRequestRepository.Setup(l => l.UpdatePaymentRequest(It.IsAny<PaymentUpdateParam>())).Returns(new PaymentUpdateResult { DetailXml = detailXml });
            paymentRequestRepository.Setup(l => l.RollbackApprovePaymentRequest(It.IsAny<ResultInfo>(), It.IsAny<string>())).Returns(new ResultInfo());
            paymentRequestRepository.Setup(l => l.GetSiteDBConfigInfoByCustId(It.IsAny<string>())).Returns(new SiteDBConfigInfo { LinkedServerName = "DBTXN" });
            paymentRequestRepository.Setup(l => l.ApprovePaymentRequest(It.IsAny<PaymentApproveParam>())).Returns(new ResultInfo());
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), null)).Returns(new AffiliateBasicInfo { AffType = 2 });

            var service = CreatePaymentRequestService(null, affiliateService.Object, paymentRequestRepository.Object);

            // Action
            var result = service.UpdatePaymentRequest(new PaymentUpdateParam { IsApproved = true }, new PaymentRequestUpdateEmail());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private static PaymentRequestService CreatePaymentRequestService(
            IEmailService emailService = null,
            IAffiliateService affiliateService = null,
            IPaymentRequestRepository paymentRequestRepository = null)
        {
            var fakeEmailService = emailService ?? Mock.Of<IEmailService>();
            var fakeAffiliateService = affiliateService ?? Mock.Of<IAffiliateService>();
            var fakePaymentRequestRepository = paymentRequestRepository ?? Mock.Of<IPaymentRequestRepository>();

            return new PaymentRequestService(fakePaymentRequestRepository, fakeAffiliateService, fakeEmailService);
        }
    }
}
