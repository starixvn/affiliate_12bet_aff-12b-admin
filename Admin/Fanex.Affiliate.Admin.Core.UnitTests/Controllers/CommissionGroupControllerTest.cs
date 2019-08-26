using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class CommissionGroupControllerTest
    {
        private static CommissionGroupController CreateCommissionGroupController(
            ISessionService sessionService = null,
            ICommissionGroupService commissionGroupService = null,
            IProductRepository productRepository = null,
            ICommGroupCommonService commGroupCommonService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeCommissionGroupService = commissionGroupService ?? Mock.Of<ICommissionGroupService>();
            var fakeProductRepository = productRepository ?? Mock.Of<IProductRepository>();
            var fakecommGroupCommonService = commGroupCommonService ?? Mock.Of<ICommGroupCommonService>();
            return new CommissionGroupController(fakeSessionService, fakeCommissionGroupService, fakeProductRepository, fakecommGroupCommonService);
        }

        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProductList()).Returns(new List<Product> { new Product { ProductId = 123, ProductName = "abc" } });

            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(c => c.GetDeductionList()).Returns(new List<Deduction> { new Deduction { DeductionId = 13, DeductionName = "ExchangeWinLossShare", DeductedPercentage = 0.05m } });

            var controller = CreateCommissionGroupController(productRepository: productRepository.Object, commGroupCommonService: commGroupCommonService.Object);
            var expectedViewModel = new CommissionGroupViewModel
            {
                ProductList = new List<Product> { new Product { ProductId = 123, ProductName = "abc" } },
                DeductionList = new List<Deduction> { new Deduction { DeductionId = 13, DeductionName = "ExchangeWinLossShare", DeductedPercentage = 0.05m } }
            };

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as CommissionGroupViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/CommissionGroup/Index.cshtml", viewName);
            Assert.Equal(expectedViewModel.ProductList, model.ProductList);
            Assert.Equal(expectedViewModel.DeductionList, model.DeductionList);
        }

        [Fact]
        public void GetCommissionGroupList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new GetCommissionGroupParam();
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetCommissionGroupList(param)).Returns(new List<CommissionGroupSetting> { new CommissionGroupSetting() }).Callback<GetCommissionGroupParam>(p => param.Nrows = 1);
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);
            var expectedViewModel = new GetCommissionGroupViewModel
            {
                CommissionSettingList = new List<CommissionGroupSetting> { new CommissionGroupSetting() },
                TotalRows = 1
            };

            // Action
            var result = controller.GetCommissionGroupList(param) as JsonNetResult;
            var model = result.Data as GetCommissionGroupViewModel;

            // Assert
            Assert.Equal(expectedViewModel.TotalRows, model.TotalRows);
            Assert.Equal(expectedViewModel.CommissionSettingList, model.CommissionSettingList);
        }

        [Fact]
        public void GetCommissionGroup_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new GetCommissionGroupParam { GroupId = 123 };
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetCommissionGroupList(It.IsAny<GetCommissionGroupParam>())).Returns(new List< CommissionGroupSetting >{ new CommissionGroupSetting { GroupName = "Test", Description = "Test" } });
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);
            var expectedResult = new CommissionGroupSetting();

            // Action
            var result = controller.GetCommissionGroup(param) as JsonNetResult;
            var model = result.Data as CommissionGroupSetting;

            // Assert
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public void GetCommissionGroup_WhenCall_ReturnNullData()
        {
            // Arrange
            var param = new GetCommissionGroupParam();
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetCommissionGroupList(param));
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);
            var expectedResult = new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = CommGroupErrorMessage.InvalidGroupId
            };

            // Action
            var result = controller.GetCommissionGroup(param) as JsonNetResult;
            var model = result.Data as ResultInfo;

            // Assert
            Assert.Equal(expectedResult.Code, model.Code);
        }

        [Fact]
        public void GetCommissionGroup_WhenCall_ExcuteUncorrectWithInvalidParam()
        {
            // Arrange
            var param = new GetCommissionGroupParam();
            var controller = CreateCommissionGroupController();
            var expectedResult = new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = CommGroupErrorMessage.InvalidGroupId
            };

            // Action
            var result = controller.GetCommissionGroup(param) as JsonNetResult;
            var model = result.Data as ResultInfo;

            // Assert
            Assert.Equal(expectedResult.Code, model.Code);
        }
    }
}
