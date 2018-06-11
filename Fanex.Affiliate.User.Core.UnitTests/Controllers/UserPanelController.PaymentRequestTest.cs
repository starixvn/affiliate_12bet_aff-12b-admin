using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.UnitTests.Core;
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
                AffId = Core.Constant.DefaultAffId,
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
            Assert.Equal(result.IsValidDateRange, expected);
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