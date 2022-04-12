using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;
using System;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class VerificationRepository : BaseRepository, IVerificationRepository
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

                return GetResultInfo(db);
            }
        }

        private static void AssignUpdateLoginResultParam(IObjectDb objectDb, AuthResultBaseParam param)
        {
            objectDb.SetParameterValue("AffId", param.AffId);
            objectDb.SetParameterValue("IpAddress", param.IpAddress);
            objectDb.SetParameterValue("DeviceInfo", param.DeviceInfo);
            objectDb.SetParameterValue("Language", param.Language);

            AssignResultOutputParam(objectDb);
        }
    }
}