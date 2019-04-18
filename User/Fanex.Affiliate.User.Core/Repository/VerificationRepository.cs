using System;
using System.Data;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class VerificationRepository : IVerificationRepository
    {
        public ResultInfo UpdateVerificationInfo(AuthResultBaseParam param)
        {
            const string SpKey = "Aff_User_VerificationUpd";

            return Execute(SpKey, param);
        }

        public VerificationInfo GetLoginInfoByUserDevice(int affId, string ipAddress, string deviceInfo)
        {
            const string SpKey = "Aff_User_VerificationInfoSel";
            VerificationInfo result = null;

            using (var objectDb = new ObjectDBBuilder(SpKey))
            {
                objectDb.DB.AddInParameter("AffId", SqlDbType.Int, affId);
                objectDb.DB.AddInParameter("IpAddress", SqlDbType.NVarChar, ipAddress);
                objectDb.DB.AddInParameter("DeviceInfo", SqlDbType.NVarChar, deviceInfo);

                using (var reader = objectDb.DB.ExecuteReader())
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
            using (var objectDbBuilder = new ObjectDBBuilder(spKey))
            {
                var objectDb = objectDbBuilder.DB;
                AssignUpdateLoginResultParam(objectDb, param);
                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue("ErrCode");

                return new ResultInfo
                {
                    IsOk = code == null || Convert.ToInt32(code) == 0,
                    Message = Convert.ToString(objectDb.GetParameterValue("ErrMsg"))
                };
            }
        }

        private static void AssignUpdateLoginResultParam(IObjectDb objectDb, AuthResultBaseParam param)
        {
            objectDb.AddInParameter("AffId", SqlDbType.Int, param.AffId);
            objectDb.AddInParameter("IpAddress", SqlDbType.NVarChar, param.IpAddress);
            objectDb.AddInParameter("DeviceInfo", SqlDbType.NVarChar, param.DeviceInfo);
            objectDb.AddInParameter("Language", SqlDbType.VarChar, param.Language);

            objectDb.AddOutParameter("ErrCode", SqlDbType.Int, DbParam.ErrCodeSize);
            objectDb.AddOutParameter("ErrMsg", SqlDbType.NVarChar, DbParam.ErrMsgSize);
        }
    }
}