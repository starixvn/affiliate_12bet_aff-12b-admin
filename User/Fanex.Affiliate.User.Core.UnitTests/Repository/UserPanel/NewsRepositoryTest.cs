using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class NewsRepositoryTest : RepositoryTestBase<NewsRepository>
    {

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetLastestHotNews_WhenCall_ReturnHotNewsAllLanguage(bool isInternal)
        {
            const string uSpKey = "Aff_HotNewsDetail";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            var reader = db.ExecuteReader();
            reader.Read().Returns(true);
            reader["Important"].Returns(true);

            var result = repo.GetLastestHotNews(isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result.HotNewsList);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetMessageList_WhenCall_ReturnMessageHistory(bool isInternal)
        {
            const string uSpKey = "Aff_HotNewsSel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var param = new MessageHistoryParam();
            param.IsInternal = isInternal;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<MessageHistory>(param).Returns(new List<MessageHistory> { new MessageHistory { } });

            var result = repo.GetMessageList(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
