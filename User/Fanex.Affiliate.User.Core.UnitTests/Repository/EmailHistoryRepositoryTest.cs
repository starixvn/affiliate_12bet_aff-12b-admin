using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository
{
    public class EmailHistoryRepositoryTest : RepositoryTestBase<EmailHistoryRepository>
    {
        [Fact]
        public void Insert_WhenDbNotThrowException_ReturnTrue()
        {
            var param = new ContactUsParam
            {
                Affiliate = "aff1",
                Browser = "Chrome",
                Captcha = "12345",
                Content = "aaaa",
                Creator = 1,
                Email = "abc@gmail.com",
                ErrCode = 1,
                ErrMsg = null,
                Ip = "127.0.0.1",
                Mid = 1,
                Sender = "abc",
                SessionId = "1234",
                Subject = "mail"
            };
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var errorCode = 101;
            db.When(d => d.ExecuteNonQuery(param))
                .Do(c => (c.Args()[0] as ContactUsParam).ErrCode = errorCode);

            repo.Insert(param);

            Assert.Equal("Aff_EmailHistoryIns", db.SettingKey);
            Assert.Equal(errorCode, param.ErrCode);
        }
    }
}
