using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class MemberControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnViewWithCorrectModel()
        {
            // Arrange
            var expectedMemberList = new List<Member> { new Member(), new Member() };
            var memberRepository = new Mock<IMemberRepository>();
            memberRepository.Setup(m => m.GetMemberList(It.IsAny<MemberParam>())).Returns(expectedMemberList);

            var controller = CreateController(memberRepository.Object);

            // Action
            var actionResult = controller.Index() as ViewResult;
            var result = actionResult.Model as IEnumerable<Member>;

            // Assert
            Assert.Equal(expectedMemberList, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Index_WhenCall_CallRepositoryWithCorrectParam(bool isInternal)
        {
            // Arrange
            MemberParam passedMemberParam = null;
            var expectedMemberList = new List<Member>();
            var memberRepository = new Mock<IMemberRepository>();
            memberRepository.Setup(m => m.GetMemberList(It.IsAny<MemberParam>()))
                .Returns(expectedMemberList)
                .Callback<MemberParam>(p => passedMemberParam = p);

            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = isInternal;

            var controller = CreateController(memberRepository.Object, sessionService);

            var expectedParam = new MemberParam
            {
                IsGetAll = true,
                AffId = Constant.DefaultAffId,
                MemberType = (byte)MemberType.All,
                PageSize = 20,
                PageIndex = 1,
                IsInternal = isInternal
            };
            var expectedResult = JsonConvert.SerializeObject(expectedParam);

            // Action
            controller.Index();
            var result = JsonConvert.SerializeObject(passedMemberParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Index_WhenCall_GetCorrectPagingInfo()
        {
            // Arrange
            const int ExpectedTotalItem = 100;
            var memberList = new List<Member>();
            var memberRepository = new Mock<IMemberRepository>();
            memberRepository.Setup(m => m.GetMemberList(It.IsAny<MemberParam>()))
                .Returns(memberList)
                .Callback<MemberParam>(p => p.NRows = ExpectedTotalItem);

            var controller = CreateController(memberRepository.Object);

            // Action
            controller.Index();
            var totalItem = (int)controller.ViewBag.TotalItem;
            var startRowNumber = (int)controller.ViewBag.StartRowNumber;

            // Assert
            Assert.Equal(ExpectedTotalItem, totalItem);
            Assert.Equal(1, startRowNumber);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetMemberList_WhenCall_CallRepositoryWithCorrectParam(bool isInternal)
        {
            // Arrange
            var memberRepository = new Mock<IMemberRepository>();

            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = isInternal;

            var controller = CreateController(memberRepository.Object, sessionService);
            var memberParam = new MemberParam();

            // Action
            controller.GetMemberList(memberParam);

            // Assert
            memberRepository.Verify(m => m.GetMemberList(memberParam), Times.Once);
            Assert.Equal(isInternal, memberParam.IsInternal);
            Assert.Equal(Constant.DefaultAffId, memberParam.AffId);
        }

        [Fact]
        public void GetMemberList_WhenCall_ReturnPartialViewWithCorrectParam()
        {
            // Arrange
            var memberList = new List<Member>();
            var memberRepository = new Mock<IMemberRepository>();
            memberRepository.Setup(m => m.GetMemberList(It.IsAny<MemberParam>())).Returns(memberList);

            var controller = CreateController(memberRepository.Object);

            const string ExpectedViewName = "~/Views/UserPanel/Member/_DataContent.cshtml";

            // Action
            var actionResult = controller.GetMemberList(new MemberParam()) as PartialViewResult;

            // Assert
            Assert.Equal(ExpectedViewName, actionResult.ViewName);
            Assert.Equal(memberList, actionResult.Model);
        }

        [Fact]
        public void GetMemberList_WhenCall_GetCorrectPagingInfo()
        {
            // Arrange
            const int ExpectedTotalItem = 100;
            const int ExpectedStartRowNumber = 21;

            var memberList = new List<Member>();
            var memberRepository = new Mock<IMemberRepository>();
            memberRepository.Setup(m => m.GetMemberList(It.IsAny<MemberParam>()))
                .Returns(memberList)
                .Callback<MemberParam>(p => p.NRows = ExpectedTotalItem);

            var controller = CreateController(memberRepository.Object);
            var memberParam = new MemberParam { PageSize = 20, PageIndex = 2 };

            // Action
            controller.GetMemberList(memberParam);
            var totalItem = (int)controller.ViewBag.TotalItem;
            var startRowNumber = (int)controller.ViewBag.StartRowNumber;

            // Assert
            Assert.Equal(ExpectedTotalItem, totalItem);
            Assert.Equal(ExpectedStartRowNumber, startRowNumber);
        }

        private static MemberController CreateController(
            IMemberRepository memberRepository,
            ISessionService sessionService = null,
            ILanguageService languageService = null)
        {
            sessionService = sessionService ?? MockingHelper.GetSessionService();
            languageService = languageService ?? Mock.Of<ILanguageService>();

            return new MemberController(sessionService, languageService, memberRepository);
        }
    }
}