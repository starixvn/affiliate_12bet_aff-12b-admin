using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.Infrastructure;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class PaymentServiceTest
    {
        [Fact]
        public void GetPaymentList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PaymentParam();
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.GetPaymentList(param)).Returns(new List<Payment> { new Payment() });
            var service = CreatePaymentService(paymentRepository: paymentRepository.Object);

            // Action
            var result = service.GetPaymentList(param);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void CancelPaymentRequest_WhenCall_AssignValueForParamCorrectly()
        {
            // Arrange
            var paymentParam = new PaymentCancelParam { UserId = Constant.DefaultAffId, IsInternal = false };
            var emailParam = new PaymentEmailParam();
            PaymentEmailParam passedEmailParam = null;

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(paymentParam)).Returns(new ResultInfo { IsOk = true });

            var userInfo = new UserInfo
            {
                UserName = "usernametest",
                AffId = Constant.DefaultAffId,
                Email = Constant.DefaultMailUser
            };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(a => a.GetUserInfoById(paymentParam.UserId, paymentParam.IsInternal)).Returns(userInfo);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                accountRepository: accountRepository.Object,
                emailService: emailService.Object);

            // Action
            service.CancelPaymentRequest(paymentParam, emailParam);

            // Assert
            Assert.Equal("Cancelled", paymentParam.Status);
            Assert.Equal(PaymentRequestType.Cancel, emailParam.PaymentType);
            Assert.Equal(userInfo.Email, emailParam.MailTo);
            Assert.Equal(JsonConvert.SerializeObject(userInfo), JsonConvert.SerializeObject(passedEmailParam.User));
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(true, null)]
        public void CancelPaymentRequest_WhenCall_ReturnCorrectResult(bool isSuccessful, string message)
        {
            // Arrange
            var paymentParam = new PaymentCancelParam { UserId = Constant.DefaultAffId, IsInternal = false };
            var emailParam = new PaymentEmailParam();

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(paymentParam)).Returns(new ResultInfo { IsOk = isSuccessful });

            UserInfo userInfo = null;
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(a => a.GetUserInfoById(paymentParam.UserId, paymentParam.IsInternal)).Returns(userInfo);

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                accountRepository: accountRepository.Object);

            // Action
            var result = service.CancelPaymentRequest(paymentParam, emailParam);

            // Assert
            Assert.Equal(isSuccessful, result.IsOk);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void CancelPaymentRequest_CancelSuccessfullyButUserInfoIsNull_DoNotSendEmail()
        {
            // Arrange
            // Arrange
            var paymentParam = new PaymentCancelParam { UserId = Constant.DefaultAffId, IsInternal = false };
            var emailParam = new PaymentEmailParam();

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CancelPaymentRequest(paymentParam)).Returns(new ResultInfo { IsOk = true });

            UserInfo userInfo = null;
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(a => a.GetUserInfoById(paymentParam.UserId, paymentParam.IsInternal)).Returns(userInfo);

            var emailService = new Mock<IEmailService>();

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                accountRepository: accountRepository.Object,
                emailService: emailService.Object);

            // Action
            service.CancelPaymentRequest(paymentParam, emailParam);

            // Assert
            emailService.Verify(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>()), Times.Never());
        }

        [Fact]
        public async Task CreatePaymentRequest_IsLocalTest_ReturnCorrectResultInfo()
        {
            // Arrange
            var param = new PaymentCreationParam
            {
                IsLocalTest = false,
                Amount = 123
            };

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(d => d.Now).Returns(new DateTime(2020, 12, 06));

            var service = CreatePaymentService(dateTimeProvider: dateTimeProvider.Object);

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(Error.PaymentPayoutTimeInvalid, result.Message);
        }

        [Fact]
        public async Task CreatePaymentRequest_InvalidAmount_ReturnCorrectResultInfo()
        {
            // Arrange
            var param = new PaymentCreationParam
            {
                IsLocalTest = true,
                Amount = 100m,
                MinAmount = 100m,
                BalBroughtForward = 90m
            };

            var service = CreatePaymentService();

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(Error.PaymentAmountInvalid, result.Message);
        }

        [Fact]
        public async Task CreatePaymentRequest_PlayerAccountIsNotActived_ReturnCorrectResultInfo()
        {
            // Arrange
            var param = CreateValidPaymentCreationParam();
            const string ErrorMessage = "Player Account Is Not Actived";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.CheckPlayerAccountIsActivedAsync(param.AffId, param.Language)).Returns(Task.FromResult(new ResultInfo { Message = ErrorMessage }));

            var service = CreatePaymentService(accountService: accountService.Object);

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public async Task CreatePaymentRequest_ValidParamButCreatePaymentFailed_ReturnCorrectResultInfo()
        {
            // Arrange
            var param = CreateValidPaymentCreationParam();
            const string ErrorMessage = "Create Payment Failed";

            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.CheckPlayerAccountIsActivedAsync(param.AffId, param.Language)).Returns(Task.FromResult(new ResultInfo { IsOk = true }));

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CreatePaymentRequest(param)).Returns(new ResultInfo { Message = ErrorMessage });

            var service = CreatePaymentService(paymentRepository: paymentRepository.Object, accountService: accountService.Object);

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.False(result.IsOk);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public async Task CreatePaymentRequest_ValidParamAndSendEmail_ReturnCorrectResultAndEmailParamAssignCorrectly()
        {
            // Arrange
            PaymentEmailParam passedEmailParam = null;
            var param = CreateValidPaymentCreationParam();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.CheckPlayerAccountIsActivedAsync(param.AffId, param.Language)).Returns(Task.FromResult(new ResultInfo { IsOk = true }));

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CreatePaymentRequest(param)).Returns(new ResultInfo { IsOk = true });

            var userInfo = new UserInfo
            {
                UserName = "usernametest",
                AffId = Constant.DefaultAffId,
                Email = Constant.DefaultMailUser
            };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(a => a.GetUserInfoById(param.AffId, param.IsInternal)).Returns(userInfo);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(
                accountRepository: accountRepository.Object,
                paymentRepository: paymentRepository.Object,
                accountService: accountService.Object,
                emailService: emailService.Object);

            var expectedEmailParam = new PaymentEmailParam
            {
                User = userInfo,
                PaymentType = PaymentRequestType.Create,
                MailTo = userInfo.Email,
                TransId = param.TransId,
                Amount = param.Amount,
                RequestDate = param.TransDate.Value
            };

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.True(result.IsOk);
            Assert.Null(result.Message);
            Assert.Equal(JsonConvert.SerializeObject(expectedEmailParam), JsonConvert.SerializeObject(passedEmailParam));
        }

        [Fact]
        public async Task CreatePaymentRequest_ValidParamButNotSendEmail_ReturnCorrectResultButEmailParamNotAssign()
        {
            // Arrange
            PaymentEmailParam passedEmailParam = null;
            var param = CreateValidPaymentCreationParam();

            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.CheckPlayerAccountIsActivedAsync(param.AffId, param.Language)).Returns(Task.FromResult(new ResultInfo { IsOk = true }));

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(p => p.CreatePaymentRequest(param)).Returns(new ResultInfo { IsOk = true });

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                accountService: accountService.Object,
                emailService: emailService.Object);

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.True(result.IsOk);
            Assert.Null(result.Message);
            Assert.Null(passedEmailParam);
        }

        private PaymentCreationParam CreateValidPaymentCreationParam()
        {
            return new PaymentCreationParam
            {
                IsLocalTest = true,
                Amount = 100m,
                MinAmount = 100m,
                BalBroughtForward = 1000m,
                AffId = Constant.DefaultAffId,
                Language = Constant.DefaultLanguage,
                TransId = 123,
                TransDate = new System.DateTime(2018, 12, 28)
            };
        }

        private static PaymentService CreatePaymentService(
            IAccountRepository accountRepository = null,
            IAccountService accountService = null,
            IPaymentRepository paymentRepository = null,
            IEmailService emailService = null,
            IDateTimeProvider dateTimeProvider = null)
        {
            var fakeAccountRepository = accountRepository ?? Mock.Of<IAccountRepository>();
            var fakeAccountService = accountService ?? Mock.Of<IAccountService>();
            var fakePaymentRepository = paymentRepository ?? Mock.Of<IPaymentRepository>();
            var fakeEmailService = emailService ?? Mock.Of<IEmailService>();
            var fakeDateTimeProvider = dateTimeProvider ?? Mock.Of<IDateTimeProvider>();

            return new PaymentService(fakeAccountRepository, fakeAccountService, fakePaymentRepository, fakeEmailService, fakeDateTimeProvider);
        }
    }

    public static class PaymentServiceTestCase
    {
        public static IEnumerable<object[]> CreateForPaymentCreation()
        {
            yield return new object[]
            {
                new PaymentCreationParam
                {
                    IsLocalTest = true,
                    Amount = 100m,
                    MinAmount = 100m,
                    BalBroughtForward = 90m
                },
                new ResultInfo { IsOk = false, Message = Error.PaymentAmountInvalid }
            };

            yield return new object[]
            {
                new PaymentCreationParam
                {
                    IsLocalTest = true,
                    Amount = 100m,
                    MinAmount = 100m,
                    BalBroughtForward = 1000m,
                    AffId = Constant.DefaultAffId,
                    Language = Constant.DefaultLanguage
                },
                new ResultInfo { IsOk = true }
            };
        }
    }
}
