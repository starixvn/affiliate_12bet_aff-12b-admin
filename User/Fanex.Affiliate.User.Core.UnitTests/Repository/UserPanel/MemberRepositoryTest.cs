using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class MemberRepositoryTest : RepositoryTestBase<MemberRepository>
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetMemberList_WhenCall_QueryMember(bool isInternal)
        {
            var param = new MemberParam();
            param.IsInternal = isInternal;
            const string uSpKey = "Aff_MemberList_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<Member>(param).Returns(new List<Member>
            {
                new Member
                {
                    CreatedDate = DateTime.Now,
                    Email = "",
                    FirstName ="",
                    LastName = "",
                    Phone = "",
                    RegisteredDate = DateTime.Now,
                    UserName = ""
                }
            });

            var result =  repo.GetMemberList(param);


            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
