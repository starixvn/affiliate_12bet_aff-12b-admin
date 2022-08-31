using Fanex.Affiliate.User.Core.Repository;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class IpAddressRepositoryTest : RepositoryTestBase<IpAddressRepository>
    {
        [Theory]
        [InlineData(false, "")]
        [InlineData(true, "")]
        [InlineData(true, "TH")]
        public void GetContryShortByIpAddress_WhenCall_ExcuteSuccess(bool isRead, string shortCode)
        {
            const string uSpKey = "Acc_GetIPInfo_Full";
            var ip = "127.0.0.1";
            var db = Builder.Build(uSpKey);
            var reader = db.ExecuteReader();
            reader.Read().Returns(isRead, false);
            reader.NextResult().Returns(true);
            reader["countrySHORT"].Returns(shortCode);
            var repo = CreateRepo();

            var result = repo.GetContryShortByIpAddress(ip);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.Equal(shortCode, result);
        }
    }
}
