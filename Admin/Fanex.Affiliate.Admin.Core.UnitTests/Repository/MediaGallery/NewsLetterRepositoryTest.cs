using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Data;
using Moq;
using System;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.MediaGallery
{
    public class NewsLetterRepositoryTest
    {
        [Fact]
        public void GetNewsLetters_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PagingParam();
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_Newsletter_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetNewsLetters(param);

            // Assert
            objectDb.Verify(obj => obj.Query<NewsLetter>(param, null, true, 120), Times.Once);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void GetNewsLetters_WhenCall_ReturnNullData()
        {
            // Arrange
            var param = new PagingParam();
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_Newsletter_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetNewsLetters(param);

            // Assert
            objectDb.Verify(obj => obj.Query<NewsLetter>(param, null, true, 120), Times.Once);
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetNewsLetterById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            repository.Setup(r => r.GetObjectDb("Aff_Admin_Newsletter_Sel", 120)).Returns(objectDb.Object);

            // Action
            repository.Object.GetNewsLetterById(123);

            // Assert
            objectDb.Verify(obj => obj.QueryEntity<NewsLetter>(It.IsAny<object>(), null, 120), Times.Once);
        }

        [Fact]
        public void InsertNewsLetter_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Newsletter_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertNewsLetter(new InsertHtmlFileParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertNewsLetter_WhenCallWithInvalidParam_InsertIsFailed()
        {
            // Arrange
            var message = "Insert the news letter is failed.";
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Newsletter_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertNewsLetter(new InsertHtmlFileParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Newsletter_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateNewsLetter(new UpdateNewsLetterParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update the news letter is failed.";
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Newsletter_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateNewsLetter(new UpdateNewsLetterParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteNewsLetter_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Newsletter_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteNewsLetter(new DeleteNewsLetterParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteNewsLetter_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Delete news letter(s) is failed.";
            var repository = new Mock<NewsLetterRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Newsletter_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteNewsLetter(new DeleteNewsLetterParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }
    }
}
