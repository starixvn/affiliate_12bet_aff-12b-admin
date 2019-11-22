namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using System;
    using System.Data;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Data;

    public class BaseRepository
    {
        protected BaseRepository()
        {
        }

        protected static void AddResultParam(IObjectDb objectDb)
        {
            objectDb.SetParameterValue("ErrCode", 4);
            objectDb.SetParameterValue("ErrMsg", 4000);
        }

        protected static ResultInfo GetResultInfo(IObjectDb objectDb)
        {
            var errorCode = objectDb.GetParameterValue("ErrCode");
            var errorMessage = objectDb.GetParameterValue("ErrMsg");

            return new ResultInfo
            {
                Code = errorCode == DBNull.Value ? 0 : Convert.ToInt32(errorCode),
                Message = errorMessage == DBNull.Value ? string.Empty : Convert.ToString(errorMessage)
            };
        }

        protected internal virtual IObjectDb GetObjectDb(string spname)
        {
            return ObjectDbFactory.CreateInstance(spname);
        }
    }
}