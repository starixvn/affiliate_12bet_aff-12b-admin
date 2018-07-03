using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels;
using Moq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class MemberTransactionControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnCorrectViewModel()
        {
            // Arrange
            var param = new MemberTransactionParam
            {
                UserName = Constant.DefaultUserName,
                Date = new System.DateTime(2018, 7, 3),
                IsDeposit = true
            };
            var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    AccountNo = "123",
                    OriginalAmount = "98.65"
                }
            };
            var service = new Mock<IMemberTransactionService>();
            service.Setup(s => s.GetTransactionInfo(param)).Returns(transactionList);
            var controller = new MemberTransactionController(service.Object);

            var expectedTotal = 98.65m;

            // Action
            var actionResult = controller.Index(param) as ViewResult;
            var result = actionResult.Model as MemberTransactionViewModel;

            // Assert
            Assert.Equal(transactionList, result.TransactionList);
            Assert.Equal(expectedTotal, result.Total);
        }

        [Fact]
        public void Index_DateHasNoValue_AssignCorrectDefaultDate()
        {
            // Arrange
            var param = new MemberTransactionParam
            {
                UserName = Constant.DefaultUserName,
                Date = null,
                IsDeposit = true
            };
            var transactionList = new List<Transaction>();
            var service = new Mock<IMemberTransactionService>();
            service.Setup(s => s.GetTransactionInfo(param)).Returns(transactionList);
            var controller = new MemberTransactionController(service.Object);

            // Action
            controller.Index(param);

            // Assert
            Assert.Equal(DateTime.Now.Date, param.Date.Value.Date); ;
        }
    }
}
