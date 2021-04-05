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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class ViewLogControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateController();

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/NewViewLog.cshtml", viewName);
        }

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
            viewLogService.Setup(s => s.GetNewViewLogList(param)).Returns(new EntityResult<NewViewLogInfo>
            {
                MainData = new List<NewViewLogInfo>
                {
                    new NewViewLogInfo {
                        AffId = 1211808,
                        AffName = "12BetAff001",
                        NumberOfAction = 123,
                        LastAction = "Action",
                        Actor = "Sa",
                        DateTime = new DateTime(2021,04,05)
                    }
                },
                TotalItem = 1
            }).Callback<NewViewLogParam>(p => passedParam = p);
            var controller = CreateController(viewLogService: viewLogService.Object);

            // Action
            var result = controller.GetNewViewLogList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<NewViewLogInfo>;
            var item = entityResult.MainData.FirstOrDefault();

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
            Assert.Equal("04/05/2021 12:00:00 AM", item.DateTimeText);
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
