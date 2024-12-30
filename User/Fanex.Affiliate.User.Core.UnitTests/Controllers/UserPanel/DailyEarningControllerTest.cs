using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using Moq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{

    public class DailyEarningControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectViewModel()
        {
            //Arrange
            var param = new DailyEarningParam
            {
                AffId = Constant.DefaultAffId,
                ProductId = 1,
                MonthDate = DateTimeHelper.CreateDateTime(2018, 11, 08),
                IsOnlyContent = false,
                IsInternal = false
            };

            var expectProductList = new List<ProductItem>
            {
                new ProductItem
                {
                    ProductId = 1,
                    ProductName = "Sportbook"
                }
            };

            var expectDailyEarningResult = new EntityResult<DailyEarning>()
            {
                MainDataList = new List<DailyEarning>(),
                Total = null
            };

            var statementHistoryService = new Mock<IStatementHistoryService>();
            statementHistoryService.Setup(s => s.GetProductList(param.IsInternal)).Returns(expectProductList);
            statementHistoryService.Setup(s => s.GetDailyEarning(param)).Returns(expectDailyEarningResult);
            var controller = CreateController(statementHistoryService.Object);

            //Action
            var actionResult = controller.Index(param) as ViewResult;
            var result = actionResult.Model as DailyEarningViewModel;

            var expectSelectedProduct = 1;
            var expectIsKoreanAgentCommGroup = false;


            //Assert
            Assert.Equal(expectSelectedProduct, result.SelectedProduct);
            Assert.Equal(expectIsKoreanAgentCommGroup, result.IsKoreanAgentCommGroup);
            Assert.Equal(expectProductList, result.ProductList);
            Assert.Equal(expectDailyEarningResult, result.Data);
        }

        [Fact]
        public void Index_ProductIdNoValue_AssignCorrectDefaultProductId()
        {
            //Arrange
            var param = new DailyEarningParam
            {
                AffId = Constant.DefaultAffId,
                ProductId = null,
                MonthDate = new DateTime(2018, 11, 08),
                IsOnlyContent = false,
                IsInternal = false
            };

            var productList = new List<ProductItem>();
            var dailyEarningResult = new EntityResult<DailyEarning>();

            var statementHistoryService = new Mock<IStatementHistoryService>();
            statementHistoryService.Setup(s => s.GetProductList(param.IsInternal)).Returns(productList);
            statementHistoryService.Setup(s => s.GetDailyEarning(param)).Returns(dailyEarningResult);
            var controller = CreateController(statementHistoryService.Object);

            var expectProductId = 1;

            //Act
            controller.Index(param);

            //Assert
            Assert.Equal(expectProductId, param.ProductId.Value);
        }

        [Fact]
        public void Index_MonthDateNoValue_AssignCorrectDefaultMonthDate()
        {
            //Arrange
            var param = new DailyEarningParam
            {
                AffId = Constant.DefaultAffId,
                ProductId = 1,
                MonthDate = null,
                IsOnlyContent = false,
                IsInternal = false
            };

            var productList = new List<ProductItem>();
            var dailyEarningResult = new EntityResult<DailyEarning>();

            var statementHistoryService = new Mock<IStatementHistoryService>();
            statementHistoryService.Setup(s => s.GetProductList(param.IsInternal)).Returns(productList);
            statementHistoryService.Setup(s => s.GetDailyEarning(param)).Returns(dailyEarningResult);
            var controller = CreateController(statementHistoryService.Object);

            //Act
            controller.Index(param);

            //Assert
            Assert.Equal(DateTime.Now.AddDays(-1).Date, param.MonthDate.Value.Date);
        }

        [Fact]
        public void Index_IsOnlyContentHasValue_AssignValueIsTrue()
        {
            //Arrange
            var param = new DailyEarningParam
            {
                AffId = Constant.DefaultAffId,
                ProductId = 1,
                MonthDate = null,
                IsOnlyContent = true,
                IsInternal = false
            };

            var productList = new List<ProductItem>();
            var dailyEarningResult = new EntityResult<DailyEarning>();

            var statementHistoryService = new Mock<IStatementHistoryService>();
            statementHistoryService.Setup(s => s.GetProductList(param.IsInternal)).Returns(productList);
            statementHistoryService.Setup(s => s.GetDailyEarning(param)).Returns(dailyEarningResult);
            var controller = CreateController(statementHistoryService.Object);

            //Act
            var actionResult = controller.Index(param) as ViewResult;
            var result = actionResult.Model as DailyEarningViewModel;

            //Assert
            Assert.Null(result.ProductList);
        }

        private static DailyEarningController CreateController(
             IStatementHistoryService statementHistoryService = null,
             ISessionService sessionService = null,
             ISystemService languageService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLanguageService = languageService ?? Mock.Of<ISystemService>();
            var fakeStatementHistoryService = statementHistoryService ?? Mock.Of<IStatementHistoryService>();
            var controller = new DailyEarningController(fakeSessionService, fakeLanguageService, fakeStatementHistoryService);

            return MockingHelper.FakeController(controller) as DailyEarningController;
        }
    }
}
