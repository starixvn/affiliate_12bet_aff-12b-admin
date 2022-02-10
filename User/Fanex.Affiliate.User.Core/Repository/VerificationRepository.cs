using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;
using System;
using System.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly IObjectDBBuilder _builder;

        public VerificationRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public ResultInfo UpdateVerificationInfo(AuthResultBaseParam param)
        {
            const string SpKey = "Aff_User_VerificationUpd";

            return Execute(SpKey, param);
        }

        public VerificationInfo GetLoginInfoByUserDevice(int affId, string ipAddress, string deviceInfo)
        {
            const string SpKey = "Aff_User_VerificationInfoSel";
            VerificationInfo result = null;

            using (var db = _builder.Build(SpKey))
            {
                db.SetParameterValue("AffId", affId);
                db.SetParameterValue("IpAddress", ipAddress);
                db.SetParameterValue("DeviceInfo", deviceInfo);

                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new VerificationInfo
                        {
                            FailedLoginCount = reader["FailedLoginCount"] == DBNull.Value ? null : (byte?)reader["FailedLoginCount"],
                            DeviceCount = Convert.ToByte(reader["DeviceCount"])
                        };
                    }
                }

                return result;
            }
        }

        public ResultInfo InsertUserDeviceInfo(AuthResultBaseParam param)
        {
            const string SpKey = "Aff_User_VerificationSuccessIns";

            return Execute(SpKey, param);
        }

        private ResultInfo Execute(string spKey, AuthResultBaseParam param)
        {
            using (var db = _builder.Build(spKey))
            {
                AssignUpdateLoginResultParam(db, param);
                db.ExecuteNonQuery();

                var code = db.GetParameterValue("ErrCode");

                return new ResultInfo
                {
                    IsOk = code == null || Convert.ToInt32(code) == 0,
                    Message = Convert.ToString(db.GetParameterValue("ErrMsg"))
                };
            }
        }

        private static void AssignUpdateLoginResultParam(IObjectDb objectDb, AuthResultBaseParam param)
        {
            objectDb.SetParameterValue("AffId", param.AffId);
            objectDb.SetParameterValue("IpAddress", param.IpAddress);
            objectDb.SetParameterValue("DeviceInfo", param.DeviceInfo);
            objectDb.SetParameterValue("Language", param.Language);

            objectDb.SetParameterValue("ErrCode", DbParam.ErrCodeSize);
            objectDb.SetParameterValue("ErrMsg", DbParam.ErrMsgSize);
        }
    }
}