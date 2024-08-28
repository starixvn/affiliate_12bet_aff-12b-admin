using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Controllers.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class CommGroupAssignmentControllerTest
    {
        private ILifetimeScopeProvider _scopeProvider;

        [Fact]
        public void AssignGroup_WhenCallWithInvalidAffId_ReturnViewWithEmptyModel()
        {
            // Arrange
            var controller = CreateCommGroupAssignmentController();

            // Action
            var viewResult = controller.AssignGroup(0) as ViewResult;
            var model = viewResult.Model as AssignNormalGroupViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/AssignGroup.cshtml", viewResult.ViewName);
            Assert.Equal(0, model.SelectedGroup);
        }

        [Fact]
        public void AssignGroup_WhenCall_ReturnView()
        {
            // Arrange
            int affId = 1271808;
            GetGroupListParam passedParam = null;
            var commGroupList = new List<SelectItem> { new SelectItem { Id = "133", Name = "Test" } };
            var commGroupAssignmentService = new Mock<ICommGroupAssignmentService>();
            commGroupAssignmentService.Setup(c => c.GetNormalCommGroupList(It.IsAny<GetGroupListParam>())).Returns(commGroupList).Callback<GetGroupListParam>(p => passedParam = p);
            commGroupAssignmentService.Setup(a => a.GetAffiliateBasicInfo(affId)).Returns(new AffiliateBasicInfo { AffId = affId });

            var controller = CreateCommGroupAssignmentController(commGroupAssignmentService: commGroupAssignmentService.Object);

            // Action
            var viewResult = controller.AssignGroup(affId) as ViewResult;
            var model = viewResult.Model as AssignNormalGroupViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/AssignGroup.cshtml", viewResult.ViewName);
            Assert.Equal(0, model.SelectedGroup);
            Assert.Equal(affId, passedParam.AffId);
        }

        [Fact]
        public void AssignGroup_WhenCallPostWithInvalidParam_ReturnError()
        {
            // Arrange
            var assignParam = new AssignGroupParam { AffId = 1271808, GroupId = 133 };
            var affInfo = new AffiliateBasicInfo();
            const string ErrorMessage1 = "ErrorMessage1";
            var controller = CreateCommGroupAssignmentController();
            controller.ModelState.AddModelError("Error01", ErrorMessage1);
            var expectResult = ResultInfo.GenerateErr(ErrorMessage1);
            // Action
            var jsonNetResult = controller.AssignGroup(assignParam, affInfo) as JsonNetResult;
            var resultInfo = jsonNetResult.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectResult.Code, resultInfo.Code);
            Assert.Equal(expectResult.Message, resultInfo.Message);
        }

        [Fact]
        public void AssignGroup_WhenCallPostWithInvalidGroupId_ReturnError()
        {
            // Arrange
            var assignParam = new AssignGroupParam { AffId = 1271808 };
            var affInfo = new AffiliateBasicInfo();
            var controller = CreateCommGroupAssignmentController();
            var expectResult = ResultInfo.GenerateErr();
            // Action
            var jsonNetResult = controller.AssignGroup(assignParam, affInfo) as JsonNetResult;
            var resultInfo = jsonNetResult.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectResult.Code, resultInfo.Code);
        }

        [Fact]
        public void AssignGroup_WhenCallPost_ExcuteCorrectly()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.Environment).Returns("UAT");
            appSetting.Setup(a => a.IsProduction).Returns(true);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var assignParam = new AssignGroupParam { AffId = 1271808, GroupId = 133 };
            var affInfo = new AffiliateBasicInfo();
            var commGroupAssignmentService = new Mock<ICommGroupAssignmentService>();
            commGroupAssignmentService.Setup(c => c.AssignGroup(It.IsAny<AssignGroupParam>(), affInfo, It.IsAny<AffInformationEmail>())).Returns(new ResultInfo());
            var controller = CreateCommGroupAssignmentController(commGroupAssignmentService: commGroupAssignmentService.Object);

            // Action
            var jsonNetResult = controller.AssignGroup(assignParam, affInfo) as JsonNetResult;
            var resultInfo = jsonNetResult.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        private static CommGroupAssignmentController CreateCommGroupAssignmentController(
            ISessionService sessionService = null,
            ICommGroupAssignmentService commGroupAssignmentService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeCommGroupAssignmentService = commGroupAssignmentService ?? Mock.Of<ICommGroupAssignmentService>();
            var controller = new CommGroupAssignmentController(fakeSessionService, fakeCommGroupAssignmentService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            var expectedPath = "~/";
            var expectedContent = "/";
            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock
                .Setup(m => m.Content(expectedPath))
                .Returns(expectedContent);

            controller.Url = urlHelperMock.Object;

            return controller;
        }

        private static CommissionGroupSetting CreateCommissionGroupSetting()
        {
            return new CommissionGroupSetting
            {
                BankingFee = new DeductionSetting { DeductionId = 12, ProductIds = "0", DeductedPercentage = 0.02m },
                Description = "Test",
                GroupId = 0,
                GroupName = "Test",
                OperationalFee = new DeductionSetting { DeductionId = 11, ProductIds = "0", DeductedPercentage = 0.07m },
                PlatformFeeList = new List<DeductionSetting>
                {
                    new DeductionSetting { DeductionId = 10, ProductIds = "1,3,4", DeductedPercentage = 0.17m }
                },
                QmTurnover = 100,
                TierSettingList = new List<TierSetting>
                {
                    new TierSetting { Commission = 0.05m, MaxAmount = 100, MaxQm = 100, MinAmount = 0, MinQm = 0, TierId = 1 },
                    new TierSetting { Commission = 0.15m, MaxAmount = 100000000000000, MaxQm = 1000000000, MinAmount = 101, MinQm = 101, TierId = 2 }
                }
            };
        }
    }
}
