using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Repository.Message;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Message
{
    public class MessageRepositoryTest
    {
        private const string SpKeyHotNewsSel = "Aff_HotNewsSel";
        private const string SpKeyHotNewsDetail = "Aff_HotNewsDetail";
        private const string SpKeyHotNewsIns = "Aff_HotNewsIns";
        private const string SpKeyHotNewsUpd = "Aff_HotNewsUpd";
        private const string SpKeyHotNewsDel = "Aff_HotNewsDel";

        [Fact]
        public void GetMessageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMessageList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb(SpKeyHotNewsSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMessageList(new MessageParam());

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void GetMessageList_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMessageList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb(SpKeyHotNewsSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMessageList(new MessageParam());

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetMessageDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(d => d.Read()).Returns(true);

            ReaderHelper.MapReaderData(dataReader, GetMessageList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb(SpKeyHotNewsDetail, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMessageDetail(37);

            // Assert
            Assert.Equal(37, result.MessageId);
        }

        [Fact]
        public void GetMessageDetail_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb(SpKeyHotNewsDetail, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetMessageDetail(37);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void InsertMessage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyHotNewsIns, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertMessage(new InsertUpdateMessageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertMessage_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update is failed.";
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyHotNewsIns, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertMessage(new InsertUpdateMessageParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateMessage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyHotNewsUpd, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateMessage(new InsertUpdateMessageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateMessage_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update is failed.";
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyHotNewsUpd, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateMessage(new InsertUpdateMessageParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteMessages_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyHotNewsDel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteMessages(new DeleteMessageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteMessages_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update is failed.";
            var repository = new Mock<MessageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyHotNewsDel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteMessages(new DeleteMessageParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        private IDictionary<string, object> GetMessageList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["msgid"] = 37,
                ["creatorname"] = "sa",
                ["PostedDate"] = new DateTime(),
                ["message_en"] = "test",
                ["Important"] = false,
                ["updatename"] = "",
                ["UpdatedDate"] = new DateTime()
            };
        }
    }
}
