using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
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
    public class AffiliateRepositoryTest
    {
        [Fact]
        public void GetAffiliateInfo_WhenCall_ReturnCorrectAfilliateInfo()
        {
            // Arrange
            var repository = new Mock<AffiliateRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedAffiliateInfo());
            objectDb.Setup(obj => obj.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAffiliateBasicInfo(1271808);

            // Assert
            Assert.Equal(1271808, result.AffId);
        }

        [Fact]
        public void GetAffiliateInfo_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<AffiliateRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(obj => obj.ExecuteReader()).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliateInfo_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetAffiliateBasicInfo(1271808);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ApproveAffiliate_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var repository = new Mock<AffiliateRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            objectDb.Setup(obj => obj.ExecuteNonQuery()).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Affiliate_Approved", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.ApproveAffiliate(new ApproveAffiliateParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private IDictionary<string, object> GetExpectedAffiliateInfo()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["AffId"] = 1271808,
                ["GroupId"] = 123,
                ["UserName"] = "12BetAff226",
                ["FirstName"] = "abc",
                ["Email"] = "abc@gmail.com",
                ["MailOption"] = true,
                ["PreferredLanguage"] = "vi-VN",
                ["PlayerUsername"] = "123abc",
                ["AffType"] = 2
            };
        }
    }
}
