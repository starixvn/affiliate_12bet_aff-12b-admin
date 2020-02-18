using System;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class BaseRepository
    {
        protected internal virtual IObjectDb GetObjectDb(string spName, int timeout = 120)
        {
            return new ObjectDb(spName, timeout);
        }

        protected static void AssignResultOutputParam(IObjectDb objectDb)
        {
            objectDb.AddOutParameter("ErrCode", DbType.Int32, Size.SizeOfInt);
            objectDb.AddOutParameter("ErrMsg", DbType.String, Size.SizeOfString);
        }

        protected static ResultInfo GetResultInfo(IObjectDb objectDb)
        {
            var code = objectDb.GetParameterValue("ErrCode");

            return new ResultInfo
            {
                Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                Message = objectDb.GetParameterValue("ErrMsg").ToString()
            };
        }
    }
}