using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class NewsLetterRepositoryTest : RepositoryTestBase<NewsLetterRepository>
    {
        [Fact]
        public void GetNewsLetterById_WhenCall_QueryNewsLetter()
        {
            const string uSpKey = "Aff_Admin_Newsletter_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey);
            db.QueryEntity<NewsLetter>().ReturnsForAnyArgs(new NewsLetter());

            repo.GetNewsLetterById(123);

            Assert.Equal(uSpKey, db.SettingKey);
            db.Received().QueryEntity<NewsLetter>(Arg.Compat.Is<object>(a => CheckPropertyValue(a, "NewsletterId", 123)));
        }

        [Fact]
        public void GetNewsLettersByDate_WhenCall_QueryNewsLetter()
        {
            var date = new DateTime(2021, 03, 29);
            const string uSpKey = "Aff_Admin_Newsletter_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey);
            db.Query<NewsLetter>().ReturnsForAnyArgs(new List<NewsLetter> { new NewsLetter() });

            var result = repo.GetNewsLettersByDate(date);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
