using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Repository.Message;
using Fanex.Affiliate.Admin.Core.Services.Message;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Message
{
    public class MessageServiceTest
    {
        [Fact]
        public void GetMessageList_WhenCallWithGetAllDate_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new MessageParam { IsGetAll = true };
            MessageParam passedParam = null;
            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(ex => ex.GetMessageList(param)).Returns(new EntityResult<MessageInfo> { MainData = new List<MessageInfo> { new MessageInfo() }, TotalItem = 1 }).Callback<MessageParam>(p => passedParam = p);

            var service = CreateExchangeRateService(messageRepository.Object);

            // Action
            var result = service.GetMessageList(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetMessageList_WhenCallWithNotGetAllDate_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new MessageParam { IsGetAll = false, FromDate = new DateTime(), ToDate = new DateTime() };
            MessageParam passedParam = null;
            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(ex => ex.GetMessageList(param)).Returns(new EntityResult<MessageInfo> { MainData = new List<MessageInfo> { new MessageInfo() }, TotalItem = 1 }).Callback<MessageParam>(p => passedParam = p);

            var service = CreateExchangeRateService(messageRepository.Object);

            // Action
            var result = service.GetMessageList(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetMessageDetail_WhenCallWithNotGetAllDate_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedMessageId = 37;
            var passedParam = 0;
            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(ex => ex.GetMessageDetail(selectedMessageId)).Returns(new MessageDetail { MessageId = selectedMessageId }).Callback<int>(p => passedParam = p);

            var service = CreateExchangeRateService(messageRepository.Object);

            // Action
            var result = service.GetMessageDetail(selectedMessageId);

            // Assert
            Assert.Equal(selectedMessageId, result.MessageId);
            Assert.Equal(selectedMessageId, passedParam);
        }

        [Fact]
        public void InsertMessage_WhenCallWithNotGetAllDate_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new InsertUpdateMessageParam();
            InsertUpdateMessageParam passedParam = null;
            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(ex => ex.InsertMessage(param)).Returns(new ResultInfo()).Callback<InsertUpdateMessageParam>(p => passedParam = p);

            var service = CreateExchangeRateService(messageRepository.Object);

            // Action
            var result = service.InsertMessage(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void UpdateMessage_WhenCallWithNotGetAllDate_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new InsertUpdateMessageParam();
            InsertUpdateMessageParam passedParam = null;
            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(ex => ex.UpdateMessage(param)).Returns(new ResultInfo()).Callback<InsertUpdateMessageParam>(p => passedParam = p);

            var service = CreateExchangeRateService(messageRepository.Object);

            // Action
            var result = service.UpdateMessage(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void DeleteMessages_WhenCallWithNotGetAllDate_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new DeleteMessageParam();
            DeleteMessageParam passedParam = null;
            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(ex => ex.DeleteMessages(param)).Returns(new ResultInfo()).Callback<DeleteMessageParam>(p => passedParam = p);

            var service = CreateExchangeRateService(messageRepository.Object);

            // Action
            var result = service.DeleteMessages(param);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        private static MessageService CreateExchangeRateService(IMessageRepository messageRepository = null)
        {
            var fakeMessageRepository = messageRepository ?? Mock.Of<IMessageRepository>();

            return new MessageService(fakeMessageRepository);
        }
    }
}
