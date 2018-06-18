using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class PaymentRequestTest
    {
        [Fact]
        public void PaymentRequest_GetPaymentList_ReturnPaymentListCorrectly()
        {
            // Arrange
            var paymentList = new List<Payment> { new Payment() };
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.GetPaymentList(It.IsAny<PaymentParam>())).Returns(paymentList);

            var controller = MockingHelper.GetUserPanelController(paymentRepository: paymentRepository.Object);

            var expectedResult = new PaymentViewModel { PaymentList = paymentList };

            // Action
            var actionResult = controller.PaymentRequest() as ViewResult;
            var result = actionResult.Model as PaymentViewModel;

            // Assert
            Assert.Equal(expectedResult.PaymentList, result.PaymentList);
        }

        [Fact]
        public void PaymentRequest_GetPaymentList_ReturnOutputInfoCorrectly()
        {
            // Arrange
            const decimal expectedBalBroughtForward = 100m, expectedMinAmount = 10m;
            const string expectedPreferredPayment = "PlayerAccount";

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.GetPaymentList(It.IsAny<PaymentParam>()))
                .Returns(new List<Payment>())
                .Callback<PaymentParam>(p =>
                {
                    p.BalBroughtForward = expectedBalBroughtForward;
                    p.MinAmount = expectedMinAmount;
                    p.PreferredPayment = expectedPreferredPayment;
                });

            var controller = MockingHelper.GetUserPanelController(paymentRepository: paymentRepository.Object);

            var expectedResult = new PaymentViewModel
            {
                BalBroughtForward = expectedBalBroughtForward,
                MinAmount = expectedMinAmount,
                PreferredPayment = expectedPreferredPayment
            };

            // Action
            var actionResult = controller.PaymentRequest() as ViewResult;
            var result = actionResult.Model as PaymentViewModel;

            // Assert
            Assert.Equal(expectedResult.BalBroughtForward, result.BalBroughtForward);
            Assert.Equal(expectedResult.MinAmount, result.MinAmount);
            Assert.Equal(expectedResult.PreferredPayment, result.PreferredPayment);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PaymentRequest_PassedParamInfo_ParamValuesAreCorrect(bool isInternal)
        {
            // Arrange
            PaymentParam passedParam = null;
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.GetPaymentList(It.IsAny<PaymentParam>()))
                .Returns(new List<Payment>())
                .Callback<PaymentParam>(p =>
                {
                    passedParam = p;
                });

            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = isInternal;

            var controller = MockingHelper.GetUserPanelController(
                sessionService: sessionService,
                paymentRepository: paymentRepository.Object);

            var param = new PaymentParam
            {
                AffId = TestHelper.Constant.DefaultAffId,
                IsInternal = isInternal
            };
            var expectedParam = JsonConvert.SerializeObject(param);

            // Action
            controller.PaymentRequest();
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            Assert.Equal(expectedParam, result);
        }

        [Theory]
        [MemberData(nameof(PaymentViewModelData.GetTestCaseForPendingPayment), MemberType = typeof(PaymentViewModelData))]
        public void PaymentRequest_CheckPendingPaymentInPaymentList_PendingPaymentHasCorrectValue(IEnumerable<Payment> paymentList, bool expected)
        {
            // Arrange
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.GetPaymentList(It.IsAny<PaymentParam>()))
                .Returns(paymentList);

            var controller = MockingHelper.GetUserPanelController(
                paymentRepository: paymentRepository.Object);

            // Action
            var actionResult = controller.PaymentRequest() as ViewResult;
            var result = actionResult.Model as PaymentViewModel;

            // Assert
            Assert.Equal(expected, result.PendingPayment != null);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(-10, 0, false)]
        [InlineData(10, 10, true)]
        [InlineData(20, 10, true)]
        [InlineData(20, 0, true)]
        public void PaymentRequest_ValidateAmount_ReturnCorrectResult(decimal balance, decimal minAmount, bool expected)
        {
            // Arrange
            PaymentParam passedParam = null;
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.GetPaymentList(It.IsAny<PaymentParam>()))
                .Returns(new List<Payment>())
                .Callback<PaymentParam>(p =>
                {
                    p.BalBroughtForward = balance;
                    p.MinAmount = minAmount;
                    passedParam = p;
                });

            var controller = MockingHelper.GetUserPanelController(
                paymentRepository: paymentRepository.Object);

            // Action
            var actionResult = controller.PaymentRequest() as ViewResult;
            var result = actionResult.Model as PaymentViewModel;

            // Assert
            Assert.Equal(expected, result.IsValidAmount);
        }

        [Theory]
        [InlineData(true, true)]
        public void PaymentRequest_ValidateDateRange_GetResultByEnvironment(bool isLocal, bool expected)
        {
            // Arrange
            var appSetting = Mock.Of<IAppSetting>(s => s.IsLocal == isLocal);
            var controller = MockingHelper.GetUserPanelController(appSetting: appSetting);

            // Action
            var actionResult = controller.PaymentRequest() as ViewResult;
            var result = actionResult.Model as PaymentViewModel;

            // Assert
            Assert.Equal(result.IsValidDate, expected);
        }

        [Fact]
        public void CancelPaymentRequest_InternalLogin_SetResultInfoCorrectly()
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = true;

            var controller = MockingHelper.GetUserPanelController(sessionService: sessionService);

            // Action
            controller.CancelPaymentRequest(new PaymentCancelParam());
            var result = controller.TempData[TempDataKey.PaymentResult] as ResultInfo;

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(result.Message, Lang.ErrAuthorized);
        }

        [Fact]
        public void CancelPaymentRequest_CallRepository_PassParamCorrectly()
        {
            // Arrange
            const long TransId = 1;
            var param = new PaymentCancelParam { TransId = TransId };
            PaymentCancelParam passedParam = null;

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(It.IsAny<PaymentCancelParam>()))
                            .Returns(new ResultInfo())
                            .Callback<PaymentCancelParam>(p => passedParam = p);

            var controller = MockingHelper.GetUserPanelController(paymentRepository: paymentRepository.Object);

            var expectedResult = JsonConvert.SerializeObject(new PaymentCancelParam
            {
                UserId = Constant.DefaultAffId,
                Language = Constant.DefaultLanguage,
                Status = "Cancelled",
                TransId = TransId,
                Ip = "::1"
            });

            // Action
            controller.CancelPaymentRequest(param);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CancelPaymentRequest_CallRepository_ReturnResultWithTempDataCorrectly()
        {
            // Arrange
            const long TransId = 1;
            var param = new PaymentCancelParam { TransId = TransId };
            var expectedResult = new ResultInfo { IsOk = false, Message = "Failed" };

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(It.IsAny<PaymentCancelParam>()))
                            .Returns(expectedResult);

            var controller = MockingHelper.GetUserPanelController(paymentRepository: paymentRepository.Object);

            // Action
            controller.CancelPaymentRequest(param);
            var result = controller.TempData[TempDataKey.PaymentResult] as ResultInfo;

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CancelPaymentRequest_CancelSuccessfully_SendEmail()
        {
            // Arrange
            const long TransId = 1;
            PaymentEmailParam emailParam = null;
            var param = new PaymentCancelParam
            {
                AffId = Constant.DefaultAffId,
                TransId = TransId,
                Amount = 100m,
                RequestDate = new DateTime(2018, 6, 12)
            };

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(It.IsAny<PaymentCancelParam>()))
                            .Returns(new ResultInfo { IsOk = true });

            var appSetting = GetMockAppSetting();
            var accountRepository = GetMockAccountRepository();
            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>()))
                .Callback<PaymentEmailParam>(p => emailParam = p);

            var controller = MockingHelper.GetUserPanelController(
                appSetting: appSetting,
                accountRepository: accountRepository,
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object);

            var expectedResult = JsonConvert.SerializeObject(GetExpectedEmailParam(PaymentRequestType.Cancel));

            // Action
            controller.CancelPaymentRequest(param);
            var result = JsonConvert.SerializeObject(emailParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CancelPaymentRequest_CancelFailed_DoNothing()
        {
            // Arrange
            const long TransId = 1;
            var param = new PaymentCancelParam
            {
                TransId = TransId,
                Amount = 100m,
                RequestDate = new DateTime(2018, 6, 12)
            };
            var resultInfo = new ResultInfo { IsOk = false };

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(It.IsAny<PaymentCancelParam>()))
                            .Returns(resultInfo);
            var accountRepository = new Mock<IAccountRepository>();
            var emailService = new Mock<IEmailService>();

            var controller = MockingHelper.GetUserPanelController(
                accountRepository: accountRepository.Object,
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object);

            // Action
            var result = controller.CancelPaymentRequest(param) as RedirectResult;

            // Assert
            accountRepository.VerifyNoOtherCalls();
            emailService.VerifyNoOtherCalls();
            Assert.Equal("#PaymentRequest", result.Url);
        }

        [Fact]
        public void CreatePaymentRequest_InternalLogin_DoNoThingAndSetResultInfoCorrectly()
        {
            // Arrange
            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = true;

            var controller = MockingHelper.GetUserPanelController(sessionService: sessionService);

            // Action
            controller.CreatePaymentRequest(new PaymentCreationParam());
            var result = controller.TempData[TempDataKey.PaymentResult] as ResultInfo;

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(result.Message, Lang.ErrAuthorized);
        }

        [Fact]
        public void CreatePaymentRequest_InvalidAmount_DoNoThingAndSetResultInfoCorrectly()
        {
            // Arrange
            var appSetting = Mock.Of<IAppSetting>(a => a.IsLocal);
            var creationParam = new PaymentCreationParam()
            {
                AffId = Constant.DefaultAffId,
                Amount = 0,
                MinAmount = 100,
                BalBroughtForward = 150
            };
            var controller = MockingHelper.GetUserPanelController(appSetting: appSetting);
            var expectedResult = new ResultInfo
            {
                IsOk = false,
                Message = Error.PaymentAmountInvalid
            };

            // Action
            controller.CreatePaymentRequest(creationParam);
            var result = controller.TempData[TempDataKey.PaymentResult] as ResultInfo;

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public void CreatePaymentRequest_CheckAccountIsActived_PassedParamCorrectly()
        {
            // Arrange
            PaymentCreationParam passedParam = null;
            var transDate = new DateTime(2018, 6, 13);
            var creationParam = new PaymentCreationParam()
            {
                AffId = Constant.DefaultAffId,
                Amount = 100,
                MinAmount = 100,
                BalBroughtForward = 150
            };

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CreatePaymentRequest(It.IsAny<PaymentCreationParam>()))
                            .Returns(new ResultInfo { IsOk = true })
                            .Callback<PaymentCreationParam>(p =>
                            {
                                p.TransDate = transDate;
                                passedParam = p;
                            });

            var appSetting = GetMockAppSetting();
            var accountRepository = GetMockAccountRepository();
            var emailService = new Mock<IEmailService>();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                .Returns(new ResultInfo { IsOk = true });

            var controller = MockingHelper.GetUserPanelController(
                appSetting: appSetting,
                accountRepository: accountRepository,
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object,
                accountService: accountService.Object);

            var expectedParam = new PaymentCreationParam
            {
                AffId = Constant.DefaultAffId,
                Amount = creationParam.Amount,
                Ip = "::1",
                Language = Constant.DefaultLanguage,
                TransDate = transDate,
                MinAmount = 100m,
                BalBroughtForward = 150m,
                IsLocalTest = true
            };
            var expectedResult = JsonConvert.SerializeObject(expectedParam);

            // Action
            controller.CreatePaymentRequest(creationParam);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            accountService.Verify(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage), Times.Once);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CreatePaymentRequest_AccountIsNotActived_ReturnCheckingResultIntermediately()
        {
            // Arrange
            var creationParam = new PaymentCreationParam()
            {
                Amount = 100,
                MinAmount = 100,
                BalBroughtForward = 150
            };
            var checkingActiveResult = new ResultInfo
            {
                IsOk = false,
                Message = "Not Actived"
            };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                .Returns(checkingActiveResult);

            var paymentRepository = new Mock<IPaymentRepository>();
            var appSetting = Mock.Of<IAppSetting>(ap => ap.IsLocal);

            var controller = MockingHelper.GetUserPanelController(
                accountService: accountService.Object,
                paymentRepository: paymentRepository.Object,
                appSetting: appSetting);

            // Action
            controller.CreatePaymentRequest(creationParam);
            var result = controller.TempData[TempDataKey.PaymentResult] as ResultInfo;

            // Assert
            paymentRepository.Verify(p => p.CreatePaymentRequest(It.IsAny<PaymentCreationParam>()), Times.Never);
            Assert.Equal(checkingActiveResult, result);
        }

        [Fact]
        public void CreatePaymentRequest_AccountIsActivedButCreatePaymentRequestFailed_ReturnCreationResultIntermediately()
        {
            // Arrange
            var creationParam = new PaymentCreationParam()
            {
                AffId = Constant.DefaultAffId,
                Amount = 100,
                MinAmount = 100,
                BalBroughtForward = 150
            };

            var accountService = new Mock<IAccountService>();
            accountService.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                .Returns(new ResultInfo { IsOk = true });

            var creationResult = new ResultInfo
            {
                IsOk = false,
                Message = "Create Failed"
            };
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CreatePaymentRequest(It.IsAny<PaymentCreationParam>())).Returns(creationResult);

            var emailService = new Mock<IEmailService>();
            var appSetting = Mock.Of<IAppSetting>(ap => ap.IsLocal);

            var controller = MockingHelper.GetUserPanelController(
                accountService: accountService.Object,
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object,
                appSetting: appSetting);

            // Action
            controller.CreatePaymentRequest(creationParam);
            var result = controller.TempData[TempDataKey.PaymentResult] as ResultInfo;

            // Assert
            emailService.Verify(p => p.SendPaymentEmail(It.IsAny<PaymentEmailParam>()), Times.Never);
            Assert.Equal(creationResult, result);
        }

        [Fact]
        public void CreatePaymentRequest_AccountIsNotActived_ReturnActionResultCorrectly()
        {
            // Arrange
            var creationParam = new PaymentCreationParam()
            {
                Amount = 100,
                MinAmount = 100,
                BalBroughtForward = 150
            };
            var checkingActiveResult = new ResultInfo
            {
                IsOk = false,
                Message = "Not Actived"
            };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                .Returns(checkingActiveResult);

            var controller = MockingHelper.GetUserPanelController(
                accountService: accountService.Object);

            const string ExpectedUrl = "#PaymentRequest";

            // Action
            var result = controller.CreatePaymentRequest(creationParam) as RedirectResult;

            // Assert
            Assert.Equal(ExpectedUrl, result.Url);
        }

        [Fact]
        public void CreatePaymentRequest_CreateSuccessfully_SendEmailWithCorrectParam()
        {
            // Arrange
            PaymentEmailParam emailParam = null;
            const int TransId = 1;
            var transDate = new DateTime(2018, 6, 12);
            var creationParam = new PaymentCreationParam()
            {
                Amount = 100,
                MinAmount = 100,
                BalBroughtForward = 150
            };

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CreatePaymentRequest(It.IsAny<PaymentCreationParam>()))
                            .Returns(new ResultInfo { IsOk = true })
                            .Callback<PaymentCreationParam>(p =>
                            {
                                p.TransDate = transDate;
                                p.TransId = TransId;
                            });

            var appSetting = GetMockAppSetting();
            var accountRepository = GetMockAccountRepository();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(ac => ac.CheckPlayerAccountIsActived(Constant.DefaultAffId, Constant.DefaultLanguage))
                .Returns(new ResultInfo { IsOk = true });

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>()))
                .Callback<PaymentEmailParam>(p => emailParam = p);

            var controller = MockingHelper.GetUserPanelController(
                appSetting: appSetting,
                accountRepository: accountRepository,
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object,
                accountService: accountService.Object);

            var expectedEmailParam = GetExpectedEmailParam(PaymentRequestType.Create);
            expectedEmailParam.TransId = TransId;
            expectedEmailParam.RequestDate = transDate;
            var expectedResult = JsonConvert.SerializeObject(expectedEmailParam);

            // Action
            controller.CreatePaymentRequest(creationParam);
            var result = JsonConvert.SerializeObject(emailParam);
            var mockAccountRepository = Mock.Get(accountRepository);

            // Assert
            mockAccountRepository.Verify(ac => ac.GetUserInfoById(Constant.DefaultAffId, false), Times.Once);
            Assert.Equal(expectedResult, result);
        }

        private IAccountRepository GetMockAccountRepository(UserInfo userInfo = null)
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(ac => ac.GetUserInfoById(Constant.DefaultAffId, false))
                .Returns(userInfo ?? GetUserInfo());

            return accountRepository.Object;
        }

        private IAppSetting GetMockAppSetting()
        {
            return Mock.Of<IAppSetting>(ap =>
                ap.IsLocal &&
                ap.MailContact == Constant.DefaultMailContact &&
                ap.MailBcc == string.Empty &&
                ap.MailCc == Constant.DefaultMailCc);
        }

        private PaymentEmailParam GetExpectedEmailParam(PaymentRequestType paymentType)
        {
            var user = GetUserInfo();

            return new PaymentEmailParam
            {
                User = user,
                Amount = 100m,
                MailBcc = "",
                MailCc = Constant.DefaultMailCc,
                MailContact = Constant.DefaultMailContact,
                MailTo = user.Email,
                RequestDate = new DateTime(2018, 6, 12),
                RootDomain = Constant.DefaultRootDomain + "/",
                TransId = 1,
                PaymentType = paymentType
            };
        }

        private UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                AffId = Constant.DefaultAffId,
                Email = Constant.DefaultMailUser,
                FirstName = "Testing",
                MailOption = false,
                PreferredLanguage = Constant.DefaultLanguage,
                UserName = "test"
            };
        }
    }

    public static class PaymentViewModelData
    {
        public static IEnumerable<object[]> GetTestCaseForPendingPayment()
        {
            yield return new object[]
            {
                new List<Payment> { new Payment { Status = PaymentStatus.Pending } },
                true
            };

            yield return new object[]
            {
                new List<Payment> { new Payment { Status = PaymentStatus.Approved } },
                false
            };

            yield return new object[]
            {
                new List<Payment>
                {
                    new Payment { Status = PaymentStatus.Approved },
                    new Payment { Status = PaymentStatus.Pending }
                },
                true
            };
        }
    }
}