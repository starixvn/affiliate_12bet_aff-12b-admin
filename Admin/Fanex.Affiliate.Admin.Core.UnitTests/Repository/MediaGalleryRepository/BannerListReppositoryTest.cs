using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.MediaGalleryRepository
{
    public class BannerListReppositoryTest
    {
        private const string SpKeyBannerSelByParentCate = "Aff_BannerSelByParentCate";
        private const string SpKeyBannerSel = "Aff_BannerSel_1";
        private const string SpKeyBannerSelDetail = "Aff_BannerSelDetail_1";
        private const string SpKeyBannerMediaIns = "Aff_BannerMediaIns";
        private const string SpKeyBannerUpd = "Aff_BannerUpd_1";
        private const string SpKeyBannerDel = "Aff_BannerDel_1";

        [Fact]
        public void GetBannerList_WhenCallWithSpKeyBannerSel_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetBannerList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb(SpKeyBannerSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetBannerList(new BannerParam(), SpKeyBannerSel);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void GetBannerList_WhenCallWithSpKeyBannerSel_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb(SpKeyBannerSel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetBannerList(new BannerParam(), SpKeyBannerSel);

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetBannerList_WhenCallWithSpKeyBannerSelByParentCate_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetBannerList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            repository.Setup(r => r.GetObjectDb(SpKeyBannerSelByParentCate, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetBannerList(new BannerParam(), SpKeyBannerSelByParentCate);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void GetBannerList_WhenCallWithSpKeyBannerSelByParentCate_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            repository.Setup(r => r.GetObjectDb(SpKeyBannerSelByParentCate, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetBannerList(new BannerParam(), SpKeyBannerSelByParentCate);

            // Assert
            Assert.Equal(0, result.TotalItem);
        }

        [Fact]
        public void GetBannerDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(d => d.Read()).Returns(true);

            ReaderHelper.MapReaderData(dataReader, GetBannerList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb(SpKeyBannerSelDetail, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetBannerDetail(123);

            // Assert
            Assert.Equal(123, result.BannerId);
        }

        [Fact]
        public void GetBannerDetail_WhenCall_ReturnNullData()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(r => r.GetObjectDb(SpKeyBannerSelDetail, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetBannerDetail(123);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void InsertBanner_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyBannerMediaIns, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertBanner(new InsertUpdateBannerParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Insert Banner is failed.";
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyBannerMediaIns, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.InsertBanner(new InsertUpdateBannerParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyBannerUpd, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateBanner(new InsertUpdateBannerParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Update Banner is failed.";
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyBannerUpd, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateBanner(new InsertUpdateBannerParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void DeleteBanners_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKeyBannerDel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteBanners(new DeleteBannerParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteBanners_WhenCallWithInvalidParam_UpdateIsFailed()
        {
            // Arrange
            var message = "Delete Banner(s) is failed.";
            var repository = new Mock<BannerListRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(-999);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(message);
            repository.Setup(re => re.GetObjectDb(SpKeyBannerDel, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.DeleteBanners(new DeleteBannerParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }
        private IDictionary<string, object> GetBannerList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["BannerID"] = 123,
                ["FileName"] = "test/abc.gìf",
                ["Category"] = "Basketball",
                ["Language"] = "English",
                ["Currency"] = "EURO",
                ["ImageType"] = "Image",
                ["AffType"] = 2,
                ["Size"] = "320x250",
                ["DeviceType"] = "PC/Laptop"
            };
        }
    }
}
