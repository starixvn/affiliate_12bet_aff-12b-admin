using Fanex.Affiliate.Admin.Core.Controllers.Member;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Member
{
    public class RegisteredControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateRegisteredController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/Member");

            controller.Url = urlHelperMock.Object;
            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Member/Index.cshtml", viewName);
        }

        [Fact]
        public void GetRegisteredMemberList_WhenCall_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new MemberParam();
            MemberParam passedParam = null;
            var memberService = new Mock<IRegisteredMemberService>();
            memberService.Setup(t => t.GetRegisteredMemberList(param)).Returns(new EntityResult<RegisteredMemberInfo> { MainData = new List<RegisteredMemberInfo> { new RegisteredMemberInfo() }, TotalItem = 1 }).Callback<MemberParam>(p => passedParam = p);
            var controller = CreateRegisteredController(memberService: memberService.Object);

            // Action
            var result = controller.GetRegisteredMemberList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<RegisteredMemberInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        private RegisteredController CreateRegisteredController(
            ISessionService sessionService = null,
            IRegisteredMemberService memberService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeMemberService = memberService ?? Mock.Of<IRegisteredMemberService>();

            return new RegisteredController(fakeSessionService, fakeMemberService);
        }
    }
}
