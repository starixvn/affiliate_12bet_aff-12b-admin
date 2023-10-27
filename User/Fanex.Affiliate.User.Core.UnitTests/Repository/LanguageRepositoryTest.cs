using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.Repository;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository
{
    public class LanguageRepositoryTest : RepositoryTestBase<SystemRepository>
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetLanguageList_WhenCall_DbQuery(bool isInternal, int userId = 123)
        {
            var uSpkey = "Aff_Admin_AdminUser_Market_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpkey + dbMode;
            var db = Builder.Build(uSpkey, dbMode);
            db.Query<Language>().ReturnsForAnyArgs(new List<Language>{
                new Language
                {
                    Status = true
                }
            });

            var repo = CreateRepo();
            var result = repo.GetLanguageList(isInternal, userId);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
