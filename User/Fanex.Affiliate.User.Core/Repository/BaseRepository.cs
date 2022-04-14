using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Data;
using System;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class BaseRepository
    {
        protected BaseRepository() { }

        protected static void AssignResultOutputParam(IObjectDb objectDb)
        {
            objectDb.SetParameterValue("ErrCode", DefaultValue.Int);
            objectDb.SetParameterValue("ErrMsg", DefaultValue.String);
        }

        protected static ResultInfo GetResultInfo(IObjectDb objectDb)
        {
            var code = objectDb.GetParameterValue("ErrCode");

            return new ResultInfo
            {
                IsOk = code == DBNull.Value || Convert.ToInt32(code) == 0,
                Message = Convert.ToString(objectDb.GetParameterValue("ErrMsg"))
            };
        }
    }
}
