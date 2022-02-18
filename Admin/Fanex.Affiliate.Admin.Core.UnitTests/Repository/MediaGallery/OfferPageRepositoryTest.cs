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
    public class OfferPageRepositoryTest
    {
        [Fact]
        public void GetOfferPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PagingParam();
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_OfferPage_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetOfferPageList(param);

            // Assert
            objectDb.Verify(obj => obj.Query<OfferPage>(param, null), Times.Once);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void GetOfferPageList_WhenCall_ReturnNullData()
        {
            // Arrange
            var param = new PagingParam();
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_OfferPage_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetOfferPageList(param);

            // Assert
            objectDb.Verify(obj => obj.Query<OfferPage>(param, null), Times.Once);
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetOfferPageById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            repository.Setup(r => r.GetObjectDb("Aff_Admin_OfferPage_Sel", 120)).Returns(objectDb.Object);

            // Action
            repository.Object.GetOfferPageById(123);

            // Assert
            objectDb.Verify(obj => obj.QueryEntity<OfferPage>(It.IsAny<object>(), null), Times.Once);
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferPage_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertOfferPage(new InsertHtmlFileParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithInvalidParam_InsertIsFailed()
        {
            // Arrange
            var message = "Insert the Offer Page is failed.";
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferPage_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertOfferPage(new InsertHtmlFileParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferPage_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateOfferPage(new UpdateOfferPageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update the Offer Page is failed.";
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferPage_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateOfferPage(new UpdateOfferPageParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteOfferPage_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferPage_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteOfferPage(new DeleteOfferPageParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteOfferPage_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Delete Offer page(s) is failed.";
            var repository = new Mock<OfferPageRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferPage_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteOfferPage(new DeleteOfferPageParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }
    }
}
