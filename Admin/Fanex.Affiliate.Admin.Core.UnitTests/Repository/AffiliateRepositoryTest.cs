using Fanex.Affiliate.Admin.Core.Repository;
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

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository
{
    public class AffiliateRepositoryTest
    {
        [Fact]
        public void GetLanguageList_WhenCall_ReturnCorrectLanguageList()
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
