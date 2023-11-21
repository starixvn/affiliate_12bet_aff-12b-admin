using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Controllers.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Customer
{
    public class MarketingNoteControllerTest
    {
        [Fact]
        public void GetMarketingNoteList_WhenCallWithValidParam_ExecuteWithCorrectData()
        {
            // Arrange
            var param = new AffiliatePagingParam { AffId = 1271808, PageIndex = 1, PageSize = 20 };
            AffiliatePagingParam passedParam = null;
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.GetNoteList(param)).Returns(new EntityResult<NoteDetail> { MainData = new List<NoteDetail> { new NoteDetail() }, TotalItem = 1 }).Callback<AffiliatePagingParam>(p => passedParam = p);
            var controller = CreateMarketingNoteController(noteService: noteService.Object);

            // Action
            var result = controller.GetMarketingNoteList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<NoteDetail>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetMarketingNoteList_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateMarketingNoteController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetMarketingNoteList(new AffiliatePagingParam()) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetMarketingNoteContentById_WhenCallWithValidParam_ExecuteWithCorrectData()
        {
            // Arrange
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.GetNoteContentById(1271808, 123)).Returns(new Note { NoteId = 123, NoteContent = "abc" });
            var controller = CreateMarketingNoteController(noteService: noteService.Object);

            // Action
            var result = controller.GetMarketingNoteContentById(1271808, 123) as JsonNetResult;
            var note = result.Data as Note;

            // Assert
            Assert.Equal(123, note.NoteId);
        }

        [Fact]
        public void GetMarketingNoteContentById_WhenCallWithValidParam_ExecuteWithNoNote()
        {
            // Arrange
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.GetNoteContentById(1271808, 123));
            var controller = CreateMarketingNoteController(noteService: noteService.Object);

            // Action
            var result = controller.GetMarketingNoteContentById(1271808, 123) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetMarketingNoteContentById_WhenCallWithInvalidParam_ReturnNull()
        {
            // Arrange
            var controller = CreateMarketingNoteController();

            // Action
            var result = controller.GetMarketingNoteContentById(0, 0) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void InsertMarketingNote_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteContent = "abc" };
            InsertUpdateNoteParam passedParam = null;
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.InsertNote(It.IsAny<InsertUpdateNoteParam>())).Returns(new ResultInfo()).Callback<InsertUpdateNoteParam>(p => passedParam = p);
            var controller = CreateMarketingNoteController(noteService: noteService.Object);

            // Action
            var result = controller.InsertMarketingNote(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.NoteId, passedParam.NoteId);
        }

        [Fact]
        public void InsertMarketingNote_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateMarketingNoteController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.InsertMarketingNote(new InsertUpdateNoteParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Insert Note is failed!", resultInfo.Message);
        }

        [Fact]
        public void UpdateMarketingNote_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteId = 1, NoteContent = "abc" };
            InsertUpdateNoteParam passedParam = null;
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.UpdateNote(It.IsAny<InsertUpdateNoteParam>())).Returns(new ResultInfo()).Callback<InsertUpdateNoteParam>(p => passedParam = p);
            var controller = CreateMarketingNoteController(noteService: noteService.Object);

            // Action
            var result = controller.UpdateMarketingNote(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.NoteId, passedParam.NoteId);
        }

        [Fact]
        public void UpdateMarketingNote_WhenCallWithInvalidMessageContent_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteContent = "abc" };
            var controller = CreateMarketingNoteController();

            // Action
            var result = controller.UpdateMarketingNote(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Update Note is failed!", resultInfo.Message);
        }

        [Fact]
        public void UpdateMarketingNote_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteId = 123 };
            var controller = CreateMarketingNoteController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateMarketingNote(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Update Note is failed!", resultInfo.Message);
        }

        private static MarketingNoteController CreateMarketingNoteController(
            ISessionService sessionService = null,
            INoteServiceBase noteService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeNoteService = noteService ?? Mock.Of<INoteServiceBase>();

            var controller = new MarketingNoteController(fakeSessionService, fakeNoteService);

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
