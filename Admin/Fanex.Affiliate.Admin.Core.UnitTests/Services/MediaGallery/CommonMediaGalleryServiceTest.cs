using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class CommonMediaGalleryServiceTest
    {
        private const string SpKeyBannerSelType = "Aff_BannerSelType_1";

        [Fact]
        public void GetMediaGalleryFilterItemList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<CommonMediaGalleryService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetMediaGalleryFilterItemList());
            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb(SpKeyBannerSelType, 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetMediaGalleryFilterItemList("test");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetMediaGalleryFilterItemList_WhenCall_ReturnNullData()
        {
            // Arrange
            var service = new Mock<CommonMediaGalleryService> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader()).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb(SpKeyBannerSelType, 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetMediaGalleryFilterItemList("test");

            // Assert
            Assert.Empty(result);
        }

        private IDictionary<string, object> GetMediaGalleryFilterItemList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = 123,
                ["Value"] = "test"
            };
        }
    }
}
