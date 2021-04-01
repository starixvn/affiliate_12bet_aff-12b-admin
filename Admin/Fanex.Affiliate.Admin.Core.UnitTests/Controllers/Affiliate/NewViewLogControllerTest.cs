using Fanex.Affiliate.Admin.Core.Controllers.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.NewViewLog;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.NewViewLog;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class ViewLogControllerTest
    {
        [Fact]
        public void GetLogActionList_WhenCall_ExcuteWithCorrectData()
        {
            // Arrange
            var viewLogService = new Mock<INewViewLogService>();
            viewLogService.Setup(s => s.GetLogActionList()).Returns(new List<LogAction> { new LogAction() });
            var controller = CreateController(viewLogService: viewLogService.Object);

            // Action
            var result = controller.GetLogActionList() as JsonNetResult;
            var entityResult = result.Data as List<LogAction>;

            // Assert
            Assert.Single(entityResult);
        }

        [Fact]
        public void GetNewViewLogList_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new NewViewLogParam { AffId = 1271808, LogActionIds = string.Empty, PageIndex = 1, PageSize = 20 };
            NewViewLogParam passedParam = null;
            var viewLogService = new Mock<INewViewLogService>();
            viewLogService.Setup(s => s.GetNewViewLogList(param)).Returns(new EntityResult<NewViewLogInfo> { MainData = new List<NewViewLogInfo> { new NewViewLogInfo() }, TotalItem = 1 }).Callback<NewViewLogParam>(p => passedParam = p);
            var controller = CreateController(viewLogService: viewLogService.Object);

            // Action
            var result = controller.GetNewViewLogList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<NewViewLogInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetNewViewLogList_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetNewViewLogList(new NewViewLogParam()) as JsonNetResult;
            var data = result.Data as EntityResult<NewViewLogInfo>;

            // Assert
            Assert.Empty(data.MainData);
        }

        [Fact]
        public void GetNewViewLogDetailList_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new NewViewLogParam { AffId = 1271808, LogActionIds = string.Empty, PageIndex = 1, PageSize = 20 };
            NewViewLogParam passedParam = null;
            var viewLogService = new Mock<INewViewLogService>();
            viewLogService.Setup(s => s.GetNewViewLogDetailList(param)).Returns(new EntityResult<NewViewLogDetailInfo> { MainData = new List<NewViewLogDetailInfo> { new NewViewLogDetailInfo() }, TotalItem = 1 }).Callback<NewViewLogParam>(p => passedParam = p);
            var controller = CreateController(viewLogService: viewLogService.Object);

            // Action
            var result = controller.GetNewViewLogDetailList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<NewViewLogDetailInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetNewViewLogDetailList_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetNewViewLogDetailList(new NewViewLogParam()) as JsonNetResult;
            var data = result.Data as EntityResult<NewViewLogDetailInfo>;

            // Assert
            Assert.Empty(data.MainData);
        }


        private ViewLogController CreateController(
            ISessionService sessionService = null,
            INewViewLogService viewLogService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeViewLogService = viewLogService ?? Mock.Of<INewViewLogService>();

            return new ViewLogController(fakeSessionService, fakeViewLogService);
        }
    }
}
