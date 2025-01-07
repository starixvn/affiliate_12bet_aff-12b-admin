using Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.Note;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
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
            dynamicRepository.Setup(r => r.Fetch<NoteInfo>(It.IsAny<NoteCriteria>(), It.IsAny<string>())).Returns(new List<NoteInfo>
            {
                new NoteInfo { LastModify = new DateTime(2019, 11, 22) }
            });

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.GetNoteList(new AffiliatePagingParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<NoteInfo>(It.Is<NoteCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
            Assert.Equal("11/22/2019 00:00:00", item.LastModifyText);
        }

        [Fact]
        public void GetNoteList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<NoteInfo>(It.IsAny<NoteCriteria>(), It.IsAny<string>())).Returns(new List<NoteInfo>
            {
                new NoteInfo ()
            });

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.GetNoteList(new AffiliatePagingParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<NoteInfo>(It.Is<NoteCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
            Assert.Equal("N/A", item.LastModifyText);
        }

        [Fact]
        public void GetNoteContentById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<NoteBasicInfo>(It.IsAny<NoteDetailCriteria>(), It.IsAny<string>())).Returns(new NoteBasicInfo { NoteId = 37 });

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.GetNoteContentById(1271808, 37);

            // Assert
            dynamicRepository.Verify(d => d.Get<NoteBasicInfo>(It.Is<NoteDetailCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Equal(37, result.NoteId);
        }

        [Fact]
        public void InsertNote_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<InsertNoteCommand>(), It.IsAny<string>()));

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.InsertNote(new InsertUpdateNoteParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<InsertNoteCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateNote_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<UpdateNoteCommand>(), It.IsAny<string>()));

            // Action
            var service = new NoteService(dynamicRepository.Object);
            var result = service.UpdateNote(new InsertUpdateNoteParam());

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<UpdateNoteCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }
    }
}
