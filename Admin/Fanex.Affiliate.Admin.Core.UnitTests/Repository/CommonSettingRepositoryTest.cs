using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository
{
    public class CommonSettingRepositoryTest
    {
        [Fact]
        public void GetProductList_WhenCall_CallCorrectMethodOfObjectDb()
        {
            // Arrange
            var objectDb = new Mock<IObjectDb>();
            var repository = new Mock<CommonSettingRepository> { CallBase = true };
            repository.Setup(r => r.GetObjectDb("Aff_ProductList_Sel", It.IsAny<int>())).Returns(objectDb.Object);

            // Action
            repository.Object.GetProductList();

            // Assert
            objectDb.Verify(obj => obj.Query<Product>(null, null), Times.Once);
        }

        [Fact]
        public void GetCountryList_WhenCall_CallCorrectMethodOfObjectDb()
        {
            // Arrange
            var objectDb = new Mock<IObjectDb>();
            var repository = new Mock<CommonSettingRepository> { CallBase = true };
            repository.Setup(r => r.GetObjectDb("Aff_Country_Sel", It.IsAny<int>())).Returns(objectDb.Object);

            // Action
            repository.Object.GetCountryList();

            // Assert
            objectDb.Verify(obj => obj.Query<Country>(null, null), Times.Once);
        }

        [Fact]
        public void GetLanguageList_WhenCall_ReturnCorrectLanguageList()
        {
            // Arrange
            var repository = new Mock<CommonSettingRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedLanguageList());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Market_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetLanguageList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAffTypeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new Mock<CommonSettingRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetAfTypeList());
            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_AffTypeSel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetAffTypeList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAffTypeList_WhenCall_ReturnNullData()
        {
            // Arrange
            var service = new Mock<CommonSettingRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false);

            objectDb.Setup(o => o.ExecuteReader(null, null)).Returns(dataReader.Object);
            service.Setup(r => r.GetObjectDb("Aff_AffTypeSel", 120)).Returns(objectDb.Object);

            // Action
            var result = service.Object.GetAffTypeList();

            // Assert
            Assert.Empty(result);
        }

        private IDictionary<string, object> GetAfTypeList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["TypeId"] = 123,
                ["TypeName"] = "Asian"
            };
        }

        private IDictionary<string, object> GetExpectedLanguageList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["MarketId"] = "en-US",
                ["MarketName"] = "English",
                ["ShortedCode"] = "en",
            };
        }
    }
}
