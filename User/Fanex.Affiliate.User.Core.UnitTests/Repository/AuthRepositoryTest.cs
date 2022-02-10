using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Data;
using NSubstitute;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository
{
    public class AuthRepositoryTest : RepositoryTestBase<AuthRepository>
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetOnUseId_WhenCall_QueryOnUserId(bool isInternal)
        {
            var repo = CreateRepo();
            var param = new OnUserIdParam
            {
                CustId = 1,
                Ip = "127.0.0.1",
                IsInternal = isInternal,
                NetSessionId = "session",
                On_UserId = 0,
                ServerId = 2,
                SubAccId = 4
            };
            var queryUserId = 100;
            var db = Builder.Build(null, null);
            db.When(c => c.ExecuteNonQuery(Arg.Compat.Any<OnUserIdParam>()))
                .Do(c =>
                {
                    (c.Args()[0] as OnUserIdParam).On_UserId = queryUserId;
                });


            repo.GetOnUserId(param);

            Assert.Equal(queryUserId, param.On_UserId);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetUserInfo_WhenDdCantReadRecord_ReturnNull(bool isInternal)
        {
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var reader = db.ExecuteReader();
            reader.Read().Returns(false);

            var userInfo = repo.GetUserInfo("username", isInternal);

            Assert.Null(userInfo);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetUserInfo_WhenDbReturnCorrectRecord_ReturnCorrectObject(bool isInternal)
        {
            var authUserInfo = new AuthorizedUserInfo
            {
                AffId = 1,
                AffType = "asia",
                GroupName = "group",
                IsClosed = false,
                OnUserId = 1,
                Password = "pass",
                PreferredLanguage = "vi-VN",
                Status = "Approved",
                UserName = "Name"
            };
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var reader = db.ExecuteReader();
            reader.Read().Returns(true);
            AssignObjectToReader(reader, authUserInfo);

            var userInfo = repo.GetUserInfo("username", isInternal);

            Assert.NotNull(userInfo);
            Assert.Equal(authUserInfo.AffId, userInfo.AffId);
        }

        [Fact]
        public void UpdateLoginResult_WhenDbNotThrowException_ReturnTrue()
        {
            var param = new LoginResultParam
            {
                AffId = 1,
                DeviceInfo = "destop",
                ErrCode = 2,
                ErrMsg = null,
                IpAddress = "127.0.0.1",
                IsSuccessful = true,
                Language = "vi-VN"
            };
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.GetParameterValue("ErrCode").Returns("0");

            var result = repo.UpdateLoginResult(param);

            AssertDbReciveParam(db, param);
            Assert.True(result.IsOk);
        }

        [Fact]
        public void GetInternalLoginInfo_WhenCall_ReturnSessionId()
        {
            var onUserId = 1;
            var sessionId = "12345";
            var spKey = "Aff_SessionIdSel";

            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var reader = db.ExecuteReader();

            reader.Read().Returns(true);
            reader["NetSessionID"].Returns(sessionId);
            reader["CustId"].Returns(123);

            var result = repo.GetInternalLoginInfo(onUserId);

            Assert.Equal(spKey, db.SettingKey);
            Assert.Equal(sessionId, result.SesionId);
            db.Received().SetParameterValue("OnUserId", onUserId);
        }

        [Fact]
        public void GetInternalLoginInfo_WhenCall_ReturnNull()
        {
            var onUserId = 1;
            var spKey = "Aff_SessionIdSel";

            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.ExecuteReader();

            var result = repo.GetInternalLoginInfo(onUserId);

            Assert.Equal(spKey, db.SettingKey);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RecheckIn_WhenCall_ReturnKickOutStatus(bool isInternal)
        {
            var spSurfix = isInternal ? DBMode.View : DBMode.Main;
            var spKey = "Aff_Recheckin" + spSurfix;
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var isKickOut = false;
            db.When(d => d.ExecuteNonQuery(Arg.Compat.Any<object>()))
                .Do(c => SetObjectPropetyValue(c.Args()[0], "IsKickedOut", isKickOut));

            var result = repo.Recheckin(1, isInternal);

            Assert.Equal(spKey, db.SettingKey);
            Assert.Equal(isKickOut, result);
        }

        [Fact]
        public void GetInternalLoginPermission_WhenCall_ReturnSessionId()
        {
            var internalLoginUserId = 1;
            var spKey = "Aff_Admin_PolicySel";

            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var reader = db.ExecuteReader();

            reader.Read().Returns(true, false);
            reader["FuncName"].Returns("[Test]");
            reader["Host"].Returns(1);

            var result = repo.GetInternalLoginPermission(internalLoginUserId);

            Assert.Equal(spKey, db.SettingKey);
            Assert.NotEmpty(result);
            db.Received().SetParameterValue("UserId", internalLoginUserId);
        }

        private static void AssertDbReciveParam(IObjectDb db, LoginResultParam param)
        {
            db.Received().SetParameterValue("AffId", param.AffId);
            db.Received().SetParameterValue("IpAddress", param.IpAddress);
            db.Received().SetParameterValue("DeviceInfo", param.DeviceInfo);
            db.Received().SetParameterValue("Language", param.Language);
            db.Received().SetParameterValue("IsSuccessful", param.IsSuccessful);

            db.Received().SetParameterValue("ErrCode", DbParam.ErrCodeSize);
            db.Received().SetParameterValue("ErrMsg", DbParam.ErrMsgSize);
        }
    }
}
