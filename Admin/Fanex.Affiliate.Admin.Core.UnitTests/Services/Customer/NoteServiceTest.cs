using Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note;
using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.Note;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class NoteServiceTest
    {
        [Fact]
        public void GetNoteList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<NoteDetail>(It.IsAny<NoteCriteria>())).Returns(new List<NoteDetail>
            {
                new NoteDetail { LastModify = new DateTime(2019, 11, 22) }
            });

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.GetNoteList(new AffiliatePagingParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("11/22/2019 00:00:00", item.LastModifyText);
        }

        [Fact]
        public void GetNoteList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<NoteDetail>(It.IsAny<NoteCriteria>())).Returns(new List<NoteDetail>
            {
                new NoteDetail ()
            });

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.GetNoteList(new AffiliatePagingParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("N/A", item.LastModifyText);
        }

        [Fact]
        public void GetNoteContentById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<Note>(It.IsAny<NoteDetailCriteria>())).Returns(new Note { NoteId = 37 });

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.GetNoteContentById(1271808, 37);

            // Assert
            Assert.Equal(37, result.NoteId);
        }

        [Fact]
        public void InsertNote_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<InsertNoteCommand>()));

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.InsertNote(new InsertUpdateNoteParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateNote_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<UpdateNoteCommand>()));

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.UpdateNote(new InsertUpdateNoteParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
