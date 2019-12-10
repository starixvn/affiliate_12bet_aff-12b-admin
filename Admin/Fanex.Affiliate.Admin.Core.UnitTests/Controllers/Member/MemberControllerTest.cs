using Fanex.Affiliate.Admin.Core.Controllers.Member;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Member;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Member
{
    public class MemberControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateMemberController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/Member");

            controller.Url = urlHelperMock.Object;
            // Action
            var viewResult = controller.Index("12BetAff226") as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as MemberListViewModel;

            // Assert
            Assert.Empty(viewName);
            Assert.True(model.IsFromOtherPages);
            Assert.Equal("12BetAff226", model.AffName);
        }

        [Fact]
        public void GetMemberList_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new MemberParam();
            MemberParam passedParam = null;
            var memberService = new Mock<IMemberService>();
            memberService.Setup(t => t.GetMemberList(param)).Returns(new EntityResult<MemberInfo> { MainData = new List<MemberInfo> { new MemberInfo() }, TotalItem = 1 }).Callback<MemberParam>(p => passedParam = p);
            var controller = CreateMemberController(memberService: memberService.Object);

            // Action
            var result = controller.GetMemberList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<MemberInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void DeleteMember_WhenCallWithValidParam_DeleteSuccessful()
        {
            // Arrange
            var param = new DeleteMemberParam { MemberId = 123 };
            DeleteMemberParam passedParam = null;
            var memberService = new Mock<IMemberService>();
            memberService.Setup(s => s.DeleteMember(param)).Returns(new ResultInfo()).Callback<DeleteMemberParam>(p => passedParam = p);
            var controller = CreateMemberController(memberService: memberService.Object);

            // Action
            var result = controller.DeleteMember(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void DeleteMember_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateMemberController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.DeleteMember(new DeleteMemberParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("No member specificed!", resultInfo.Message);
        }

        private MemberController CreateMemberController(
            ISessionService sessionService = null,
            IMemberService memberService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeMemberService = memberService ?? Mock.Of<IMemberService>();

            var controller = new MemberController(fakeSessionService, fakeMemberService);

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
