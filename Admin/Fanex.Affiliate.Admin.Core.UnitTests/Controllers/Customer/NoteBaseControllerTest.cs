using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Constants;
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
    public class NoteBaseControllerTest
    {
        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void GetList_WhenCallWithValidParam_ExecuteWithCorrectData(string noteType)
        {
            // Arrange
            var param = new AffiliatePagingParam { AffId = 1271808, PageIndex = 1, PageSize = 20 };
            AffiliatePagingParam passedParam = null;
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.GetNoteList(param)).Returns(new EntityResult<NoteDetail> { MainData = new List<NoteDetail> { new NoteDetail() }, TotalItem = 1 }).Callback<AffiliatePagingParam>(p => passedParam = p);
            var controller = CreateNoteBaseController(noteType, noteService: noteService.Object);

            // Action
            var result = controller.GetList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<NoteDetail>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void GetList_WhenCallWithInvalidParam_ReturnError(string noteType)
        {
            // Arrange
            var controller = CreateNoteBaseController(noteType);
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetList(new AffiliatePagingParam()) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void GetContentById_WhenCallWithValidParam_ExecuteWithCorrectData(string noteType)
        {
            // Arrange
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.GetNoteContentById(1271808, 123)).Returns(new Note { NoteId = 123, NoteContent = "abc" });
            var controller = CreateNoteBaseController(noteType, noteService: noteService.Object);

            // Action
            var result = controller.GetContentById(1271808, 123) as JsonNetResult;
            var note = result.Data as Note;

            // Assert
            Assert.Equal(123, note.NoteId);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void GetContentById_WhenCallWithValidParam_ExecuteWithNoNote(string noteType)
        {
            // Arrange
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.GetNoteContentById(1271808, 123));
            var controller = CreateNoteBaseController(noteType, noteService: noteService.Object);

            // Action
            var result = controller.GetContentById(1271808, 123) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void GetContentById_WhenCallWithInvalidParam_ReturnNull(string noteType)
        {
            // Arrange
            var controller = CreateNoteBaseController(noteType);

            // Action
            var result = controller.GetContentById(0, 0) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void Insert_WhenCallWithValidParam_InsertSuccessful(string noteType)
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteContent = "abc" };
            InsertUpdateNoteParam passedParam = null;
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.InsertNote(It.IsAny<InsertUpdateNoteParam>())).Returns(new ResultInfo()).Callback<InsertUpdateNoteParam>(p => passedParam = p);
            var controller = CreateNoteBaseController(noteType, noteService: noteService.Object);

            // Action
            var result = controller.Insert(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.NoteId, passedParam.NoteId);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void Insert_WhenCallWithInvalidParam_ReturnError(string noteType)
        {
            // Arrange
            var controller = CreateNoteBaseController(noteType);
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.Insert(new InsertUpdateNoteParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Insert Note is failed!", resultInfo.Message);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void Update_WhenCallWithValidParam_InsertSuccessful(string noteType)
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteId = 1, NoteContent = "abc" };
            InsertUpdateNoteParam passedParam = null;
            var noteService = new Mock<INoteServiceBase>();
            noteService.Setup(s => s.UpdateNote(It.IsAny<InsertUpdateNoteParam>())).Returns(new ResultInfo()).Callback<InsertUpdateNoteParam>(p => passedParam = p);
            var controller = CreateNoteBaseController(noteType, noteService: noteService.Object);

            // Action
            var result = controller.Update(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.NoteId, passedParam.NoteId);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void Update_WhenCallWithInvalidMessageContent_ReturnError(string noteType)
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteContent = "abc" };
            var controller = CreateNoteBaseController(noteType);

            // Action
            var result = controller.Update(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Update Note is failed!", resultInfo.Message);
        }

        [Theory]
        [InlineData(NoteType.AffNote)]
        [InlineData(NoteType.MarketingNote)]
        public void Update_WhenCallWithInvalidParam_ReturnError(string noteType)
        {
            // Arrange
            var param = new InsertUpdateNoteParam { NoteId = 123 };
            var controller = CreateNoteBaseController(noteType);
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.Update(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Update Note is failed!", resultInfo.Message);
        }

        private static NoteBaseController CreateNoteBaseController(
            string noteType,
            ISessionService sessionService = null,
            INoteServiceBase noteService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeNoteService = noteService ?? Mock.Of<INoteServiceBase>();

            var controller = new NoteBaseController(fakeSessionService, fakeNoteService, noteType);

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
