using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class FeedbackRepositoryTest : RepositoryTestBase<FeedbackRepository>
    {
        [Fact]
        public void GetRateList_WhenCall_ReturnCorrectData()
        {
            const string uSpKey = "Aff_RateSel";
            var db = Builder.Build(uSpKey);
            db.Query<RateInfo>().ReturnsForAnyArgs(new List<RateInfo> {
                new RateInfo()
            });
            var repo = CreateRepo();

            var result = repo.GetRateList();

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Insert_WhenCall_ExcuteSuccess()
        {
            const string uSpKey = "Aff_FeedbackIns";
            var param = new FeedbackParam { };
            var db = Builder.Build(uSpKey);
            db.When(d => d.ExecuteNonQuery(param)).Do(c => (c.Args()[0] as FeedbackParam).ErrCode = 0);
            var repo = CreateRepo();

            var result = repo.Insert(param);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.True(result.IsOk);
        }

        [Fact]
        public void IsShowFeedback_WhenCall_ReturnCorrectData()
        {
            const string uSpKey = "Aff_FeedbackPopup_Check";
            var db = Builder.Build(uSpKey);
            db.When(d => d.ExecuteNonQuery(Arg.Compat.Any<object>()))
                .Do(c => SetObjectPropetyValue(c.Args()[0], "IsShowPopup", true));
            var repo = CreateRepo();

            var result = repo.IsShowFeedback(123);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.True(result);
        }
    }
}
