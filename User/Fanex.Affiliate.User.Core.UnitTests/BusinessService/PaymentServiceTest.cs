namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Commands.Payment;
    using Core.Utilities;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using Enum;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Infrastructure;
    using MetalModels.Email;
    using MetalModels.UserPanel;
    using Moq;
    using Newtonsoft.Json;
    using Resources;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TestHelper;
    using Xunit;

    public class PaymentServiceTest
    {
        [Fact]
        public void GetPaymentList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PaymentParam();
            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Fetch<Payment>(It.IsAny<GetPaymentListCriteria>(), string.Empty)).Returns(new List<Payment> { new Payment() });
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
            var paymentParam = new PaymentCancelParam { AffId = Constant.DefaultAffId, IsInternal = false };
            var emailParam = new PaymentEmailParam();
            PaymentEmailParam passedEmailParam = null;

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CancelPaymentRequestCommand>(), string.Empty)).Returns(0)
                .Callback<CancelPaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = 0;
                    c.ErrMsg = string.Empty;
                });

            var userInfo = new UserInfo
            {
                UserName = "usernametest",
                AffId = Constant.DefaultAffId,
                Email = Constant.DefaultMailUser
            };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetUserInfoById(paymentParam.AffId, paymentParam.IsInternal)).Returns(userInfo);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(paymentRepository.Object, emailService.Object, accountService.Object);

            // Action
            service.CancelPaymentRequest(paymentParam, emailParam);

            // Assert
            Assert.Equal("Cancelled", paymentParam.Status);
            Assert.Equal(PaymentRequestType.Cancel, emailParam.PaymentType);
            Assert.Equal(userInfo.Email, emailParam.MailTo);
            Assert.Equal(JsonConvert.SerializeObject(userInfo), JsonConvert.SerializeObject(passedEmailParam.User));
        }

        [Theory]
        [InlineData(false, 100, null)]
        [InlineData(true, 0, null)]
        public void CancelPaymentRequest_WhenCall_ReturnCorrectResult(bool isSuccessful, int errCode, string message)
        {
            // Arrange
            var paymentParam = new PaymentCancelParam { AffId = Constant.DefaultAffId, IsInternal = false };
            var emailParam = new PaymentEmailParam();

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CancelPaymentRequestCommand>(), string.Empty)).Returns(0)
                .Callback<CancelPaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = errCode;
                    c.ErrMsg = message;
                });

            UserInfo userInfo = null;
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetUserInfoById(paymentParam.AffId, paymentParam.IsInternal)).Returns(userInfo);

            var service = CreatePaymentService(paymentRepository.Object, null, accountService.Object);

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

            var paymentParam = new PaymentCancelParam { AffId = Constant.DefaultAffId, IsInternal = false };
            var emailParam = new PaymentEmailParam();

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CancelPaymentRequestCommand>(), string.Empty)).Returns(0)
                .Callback<CancelPaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = 0;
                    c.ErrMsg = string.Empty;
                });

            UserInfo userInfo = null;
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetUserInfoById(paymentParam.AffId, paymentParam.IsInternal)).Returns(userInfo);

            var emailService = new Mock<IEmailService>();

            var service = CreatePaymentService(paymentRepository.Object, emailService.Object, accountService.Object);

            // Action
            service.CancelPaymentRequest(paymentParam, emailParam);

            // Assert
            emailService.Verify(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>()), Times.Never());
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
        public async Task CreatePaymentRequest_ValidParamButCreatePaymentFailed_ReturnCorrectResultInfo()
        {
            // Arrange
            var param = CreateValidPaymentCreationParam();
            const string ErrorMessage = "Create Payment Failed";

            var userInfo = CreateUserInfo();
            var accountInfo = CreateAccountInfo();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetUserInfoById(param.AffId, param.IsInternal)).Returns(userInfo);
            accountService.Setup(a => a.GetAccountInfo(param.AffId)).Returns(accountInfo);
            accountService.Setup(a => a.CheckAndUnlockUserIfLocked(accountInfo.PlayerUsername)).ReturnsAsync(new ResultInfo { IsOk = true });

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CreatePaymentRequestCommand>(), string.Empty))
                .Returns(0)
                .Callback<CreatePaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = 100;
                    c.ErrMsg = ErrorMessage;
                });

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

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CreatePaymentRequestCommand>(), string.Empty))
                .Returns(0)
                .Callback<CreatePaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = 0;
                    c.ErrMsg = string.Empty;
                    c.TransId = param.TransId;
                });

            var userInfo = CreateUserInfo();
            var accountInfo = CreateAccountInfo();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetUserInfoById(param.AffId, param.IsInternal)).Returns(userInfo);
            accountService.Setup(a => a.GetAccountInfo(param.AffId)).Returns(accountInfo);
            accountService.Setup(a => a.CheckAndUnlockUserIfLocked(accountInfo.PlayerUsername)).ReturnsAsync(new ResultInfo { IsOk = true });

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object,
                accountService: accountService.Object);

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
            Assert.Equal(JsonConvert.SerializeObject(expectedEmailParam), JsonConvert.SerializeObject(passedEmailParam));
        }

        [Fact]
        public async Task CreatePaymentRequest_ValidParamHasTransDateIsNullAndSendEmail_ReturnCorrectResultAndEmailParamAssignCorrectly()
        {
            // Arrange
            PaymentEmailParam passedEmailParam = null;
            var param = CreatePaymentCreationParamHasTransDateIsNull();

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CreatePaymentRequestCommand>(), string.Empty))
                .Returns(0)
                .Callback<CreatePaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = 0;
                    c.ErrMsg = string.Empty;
                    c.TransId = param.TransId;
                });

            var userInfo = CreateUserInfo();
            var accountInfo = CreateAccountInfo();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetUserInfoById(param.AffId, param.IsInternal)).Returns(userInfo);
            accountService.Setup(a => a.GetAccountInfo(param.AffId)).Returns(accountInfo);
            accountService.Setup(a => a.CheckAndUnlockUserIfLocked(accountInfo.PlayerUsername)).ReturnsAsync(new ResultInfo { IsOk = true });

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object,
                accountService: accountService.Object);

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.True(result.IsOk);
            Assert.NotEqual(param.TransDate, passedEmailParam.RequestDate);
        }

        [Fact]
        public async Task CreatePaymentRequest_ValidParamButNotSendEmail_ReturnCorrectResultButEmailParamNotAssign()
        {
            // Arrange
            PaymentEmailParam passedEmailParam = null;
            var param = CreateValidPaymentCreationParam();

            var paymentRepository = new Mock<IDynamicRepository>();
            paymentRepository.Setup(p => p.Execute(It.IsAny<CreatePaymentRequestCommand>(), string.Empty))
                .Returns(0)
                .Callback<CreatePaymentRequestCommand, string>((c, ip) =>
                {
                    c.ErrCode = 0;
                    c.ErrMsg = string.Empty;
                    c.TransId = param.TransId;
                });

            var accountInfo = CreateAccountInfo();
            var accountService = new Mock<IAccountService>();
            accountService.Setup(a => a.GetAccountInfo(param.AffId)).Returns(accountInfo);
            accountService.Setup(a => a.CheckAndUnlockUserIfLocked(accountInfo.PlayerUsername)).ReturnsAsync(new ResultInfo { IsOk = true });

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendPaymentEmail(It.IsAny<PaymentEmailParam>())).Callback<PaymentEmailParam>(p => passedEmailParam = p);

            var service = CreatePaymentService(
                paymentRepository: paymentRepository.Object,
                emailService: emailService.Object,
                accountService: accountService.Object);

            // Action
            var result = await service.CreatePaymentRequestAsync(param, new PaymentEmailParam());

            // Assert
            Assert.True(result.IsOk);
            Assert.Null(passedEmailParam);
        }

        private static UserInfo CreateUserInfo()
        {
            return new UserInfo
            {
                UserName = "usernametest",
                AffId = Constant.DefaultAffId,
                Email = Constant.DefaultMailUser
            };
        }

        private static AccountInformation CreateAccountInfo()
            => new AccountInformation
            {
                Address = "abc",
                AffCurrency = "USD",
                AffId = 1,
                City = "ab",
                PlayerUsername = "username"
            };

        private static PaymentCreationParam CreateValidPaymentCreationParam()
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
                TransDate = DateTimeHelper.CreateDateTime(2018, 12, 28)
            };
        }

        private static PaymentCreationParam CreatePaymentCreationParamHasTransDateIsNull()
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
                TransDate = null
            };
        }

        private static PaymentService CreatePaymentService(
            IDynamicRepository paymentRepository = null,
            IEmailService emailService = null,
            IAccountService accountService = null,
            IDateTimeProvider dateTimeProvider = null)
        {
            var fakePaymentRepository = paymentRepository ?? Mock.Of<IDynamicRepository>();
            var fakeEmailService = emailService ?? Mock.Of<IEmailService>();
            var fakeAccountService = accountService ?? Mock.Of<IAccountService>();
            var fakeIDateTimeProvider = dateTimeProvider ?? Mock.Of<IDateTimeProvider>();

            return new PaymentService(fakePaymentRepository, fakeAccountService, fakeEmailService, fakeIDateTimeProvider);
        }
    }
}
