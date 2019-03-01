using System;
using System.Data;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class VerificationRepository : IVerificationRepository
    {
        public ResultInfo UpdateVerificationInfo(AuthResultBaseParam param)
        {
            const string SpKey = "Aff_User_VerificationUpd";

            using (var objectDbBuilder = new ObjectDBBuilder(SpKey))
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

            objectDb.AddOutParameter("ErrCode", SqlDbType.Int, 4);
            objectDb.AddOutParameter("ErrMsg", SqlDbType.NVarChar, 2000);
        }
    }
}
