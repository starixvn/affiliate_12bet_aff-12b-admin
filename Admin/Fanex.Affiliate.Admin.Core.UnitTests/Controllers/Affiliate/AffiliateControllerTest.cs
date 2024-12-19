using Fanex.Affiliate.Admin.Core.Controllers.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Affiliate;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class AffiliateControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var commissionGroupRepository = new Mock<ICommGroupCommonService>();
            commissionGroupRepository.Setup(c => c.GetNormalCommGroupList(It.IsAny<GetGroupListParam>())).Returns(new List<NormalCommGroup> { new NormalCommGroup() });
            var languageService = new Mock<ISystemService>();
            languageService.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language() });

            var controller = CreateAffiliateController(commissionGroupRepository: commissionGroupRepository.Object, languageService: languageService.Object);

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/Affiliate");

            controller.Url = urlHelperMock.Object;
            // Action
            var viewResult = controller.Index(new ParamFromHome { ParentAffName = "12BETAFF001" }) as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as AffiliateInfoViewModel;

            // Assert
            Assert.Equal("", viewName);
            Assert.Single(model.CommGroupList);
            Assert.Single(model.LanguageList);
        }

        [Fact]
        public void GetAffiliateList_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new AffiliateInfoParam();
            AffiliateInfoParam passedParam = null;
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(t => t.GetAffiliateList(param)).Returns(new EntityResult<AffiliateInfo> { MainData = new List<AffiliateInfo> { new AffiliateInfo() }, TotalItem = 1 }).Callback<AffiliateInfoParam>(p => passedParam = p);
            var controller = CreateAffiliateController(affiliateService: affiliateService.Object);

            // Action
            var result = controller.GetAffiliateList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<AffiliateInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetAffiliateList_WhenCallWithInvalidParam_ReturnEmptyData()
        {
            // Arrange
            var controller = CreateAffiliateController();

            // Action
            var result = controller.GetAffiliateList(new AffiliateInfoParam { AffCode = "abc" }) as JsonNetResult;
            var entityResult = result.Data as EntityResult<AffiliateInfo>;

            // Assert
            Assert.Empty(entityResult.MainData);
            Assert.Equal(0, entityResult.TotalItem);
        }

        [Fact]
        public void ToggleFavorite_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new FavoriteParam { AffId = 123 };
            FavoriteParam passedParam = null;
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(s => s.ToggleFavorite(It.IsAny<FavoriteParam>())).Returns(new ResultInfo()).Callback<FavoriteParam>(p => passedParam = p);
            var controller = CreateAffiliateController(affiliateService: affiliateService.Object);

            // Action
            var result = controller.ToggleFavorite(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.AffId, passedParam.AffId);
        }

        [Fact]
        public void ToggleFavorite_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateAffiliateController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.ToggleFavorite(new FavoriteParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Update Favorite failed!", resultInfo.Message);
        }

        [Fact]
        public void ChangePassword_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new PasswordParam { AffId = 123, Password = "123abc" };
            PasswordParam passedParam = null;
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(s => s.ChangePassword(It.IsAny<PasswordParam>())).Returns(new ResultInfo()).Callback<PasswordParam>(p => passedParam = p);
            var controller = CreateAffiliateController(affiliateService: affiliateService.Object);

            // Action
            var result = controller.ChangePassword(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.AffId, passedParam.AffId);
        }

        [Fact]
        public void ChangePassword_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateAffiliateController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.ChangePassword(new PasswordParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Change Password is failed!", resultInfo.Message);
        }

        private AffiliateController CreateAffiliateController(
            ISessionService sessionService = null,
            ISystemService languageService = null,
            IAffiliateService affiliateService = null,
            ICommGroupCommonService commissionGroupRepository = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLanguageService = languageService ?? Mock.Of<ISystemService>();
            var fakeAffiliateService = affiliateService ?? Mock.Of<IAffiliateService>();
            var fakeCommissionGroupRepository = commissionGroupRepository ?? Mock.Of<ICommGroupCommonService>();

            var controller = new AffiliateController(fakeSessionService, fakeAffiliateService, fakeCommissionGroupRepository, fakeLanguageService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }
    }
}