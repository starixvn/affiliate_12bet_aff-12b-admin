using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class OfferRepositoryTest : RepositoryTestBase<OfferRepository>
    {
        [Fact]
        public void GetOfferById_WhenCall_QueryOffer()
        {
            const string uSpKey = "Aff_Admin_OfferPage_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey);
            db.QueryEntity<Offer>().ReturnsForAnyArgs(new Offer());

            repo.GetOfferById(123);

            Assert.Equal(uSpKey, db.SettingKey);
            db.Received().QueryEntity<Offer>(Arg.Compat.Is<object>(a => CheckPropertyValue(a, "pageId", 123)));
        }
    }
}
