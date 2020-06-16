using System;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Data;
using NSubstitute;
using Xunit;
using System.Data;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using System.Collections;

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
            var db = Builder.Build(null,null);
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
        public void GetSessionId_WhenCall_ReturnSessionId()
        {
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            var sessionId = "12345";
            var spKey = "Aff_SessionIdSel";
            db.ExecuteScalar().Returns(sessionId);

            var result = repo.GetSessionId(1);

            Assert.Equal(spKey, db.SettingKey);
            db.Received().AddInParameter("OnUserId", System.Data.SqlDbType.Int, 1);
            Assert.Equal(sessionId, result);
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

        private static void AssertDbReciveParam(IObjectDb db, LoginResultParam param)
        {
            db.Received().AddInParameter("AffId", SqlDbType.Int, param.AffId);
            db.Received().AddInParameter("IpAddress", SqlDbType.NVarChar, param.IpAddress);
            db.Received().AddInParameter("DeviceInfo", SqlDbType.NVarChar, param.DeviceInfo);
            db.Received().AddInParameter("Language", SqlDbType.VarChar, param.Language);
            db.Received().AddInParameter("IsSuccessful", SqlDbType.Bit, param.IsSuccessful);

            db.Received().AddOutParameter("ErrCode", SqlDbType.Int, DbParam.ErrCodeSize);
            db.Received().AddOutParameter("ErrMsg", SqlDbType.NVarChar, DbParam.ErrMsgSize);
        }
    }
}
