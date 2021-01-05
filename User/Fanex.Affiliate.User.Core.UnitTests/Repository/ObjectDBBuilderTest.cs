using Fanex.Affiliate.User.Core.Repository;
using Fanex.Data;
using NSubstitute;
using System.IO;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository
{
    public class ObjectDBBuilderTest
    {
        [Fact]
        public void Build_WhenCall_ReturnDbWithSettingBuild()
        {
            var builder = new ObjectDBBuilder();
            var spkey = "abc";
            var dbMode = "12345";
            var key = spkey + dbMode;
            var session = DbSettingProviderManager.StartTestingSession();
            session.Use(Substitute.For<DbSettingProviderBase>(Directory.GetCurrentDirectory()));

            var db = builder.Build(spkey, dbMode);

            Assert.Equal(key, db.SettingKey);
        }
    }
}
