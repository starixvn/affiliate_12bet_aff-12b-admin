using Fanex.Affiliate.Admin.Core.Commands.Message;
using Fanex.Affiliate.Admin.Core.Criterias.Message;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Services.Message;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Message
{
    public class MessageServiceTest
    {
        [Fact]
        public void GetMessageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Fetch<MessageInfo>(It.IsAny<MessageCriteria>(), It.IsAny<string>())).Returns(new List<MessageInfo>
            {
                new MessageInfo
                {
                    PostedDate = new DateTime(2019, 09, 18),
                    UpdatedDate = new DateTime(2019, 09, 19)
                }
            });
            var service = new MessageService(dynamicRepository.Object);

            // Action
            var result = service.GetMessageList(new MessageParam());
            var messageInfo = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("09/18/2019 00:00:00", messageInfo.PostedDateText);
            Assert.Equal("09/19/2019 00:00:00", messageInfo.UpdatedDateText);
        }

        [Fact]
        public void GetMessageList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Fetch<MessageInfo>(It.IsAny<MessageCriteria>(), It.IsAny<string>())).Returns(new List<MessageInfo>
            {
                new MessageInfo()
            });
            var service = new MessageService(dynamicRepository.Object);

            // Action
            var result = service.GetMessageList(new MessageParam());
            var messageInfo = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Null(messageInfo.PostedDateText);
            Assert.Null(messageInfo.UpdatedDateText);
        }

        [Fact]
        public void GetMessageDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Get<MessageDetail>(It.IsAny<MessageDetailCriteria>(), It.IsAny<string>())).Returns(new MessageDetail());
            var service = new MessageService(dynamicRepository.Object);

            // Action
            var result = service.GetMessageDetail(37);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void InsertMessage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<InsertMessageCommand>(), It.IsAny<string>()));
            var service = new MessageService(dynamicRepository.Object);

            // Action
            var result = service.InsertMessage(new InsertUpdateMessageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateMessage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<UpdateMessageCommand>(), It.IsAny<string>()));
            var service = new MessageService(dynamicRepository.Object);

            // Action
            var result = service.UpdateMessage(new InsertUpdateMessageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteMessages_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<DeleteMessageCommand>(), It.IsAny<string>()));
            var service = new MessageService(dynamicRepository.Object);

            // Action
            var result = service.DeleteMessages(new DeleteMessageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
