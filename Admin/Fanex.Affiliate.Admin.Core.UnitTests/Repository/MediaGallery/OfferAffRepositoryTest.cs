using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Data;
using Moq;
using System;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.MediaGallery
{
    public class OfferAffRepositoryTest
    {
        [Fact]
        public void GetOfferAffList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new SelectOfferAffParam();
            var repository = new Mock<OfferAffRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_OfferAffiliate_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetOfferAffList(param);

            // Assert
            objectDb.Verify(obj => obj.Query<OfferAff>(param, null), Times.Once);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void GetOfferAffList_WhenCall_ReturnNullData()
        {
            // Arrange
            var param = new SelectOfferAffParam();
            var repository = new Mock<OfferAffRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_OfferAffiliate_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetOfferAffList(param);

            // Assert
            objectDb.Verify(obj => obj.Query<OfferAff>(param, null), Times.Once);
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void InsertOfferAff_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<OfferAffRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferAffiliate_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertOfferAff(new InsertOfferAffParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertOfferAff_WhenCallWithInvalidParam_InsertIsFailed()
        {
            // Arrange
            var message = "Insert the Offer Page is failed.";
            var repository = new Mock<OfferAffRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferAffiliate_Ins", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertOfferAff(new InsertOfferAffParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteOfferAffs_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<OfferAffRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferAffiliate_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteOfferAffs(new DeleteOfferAffParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteOfferAffs_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Delete Offer Aff(s) is failed.";
            var repository = new Mock<OfferAffRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_OfferAffiliate_Del", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteOfferAffs(new DeleteOfferAffParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }
    }
}
