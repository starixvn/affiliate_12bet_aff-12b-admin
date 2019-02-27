using System;
using System.Data;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;

namespace Fanex.Affiliate.DataService.Job.Repository
{
    public class TimeTrackerRepository : BaseRepository, ITimeTrackerRepository
    {
        public DateTime? GetLastTimeCallSuccess(TimeTrackerBaseParam param)
        {
            const string SpKey = "Task_APIProduct_CallTimeByProductSel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("ProductId", DbType.Int32, param.ProductId);
                objectDb.AddInParameter("Currency", DbType.Int32, param.Currency);

                return (DateTime?)objectDb.ExecuteScalar();
            }
        }

        public void SaveTime(InsertTimeTrackerParam param)
        {
            const string SpKey = "Task_APIProduct_CallTimeIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("ProductId", DbType.Int32, param.ProductId);
                objectDb.AddInParameter("Currency", DbType.Int32, param.Currency);
                objectDb.AddInParameter("CallTime", DbType.DateTime, param.CallTime);
                objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
                objectDb.AddOutParameter("ErrMsg", DbType.String, 4000);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue("ErrCode");
                var errorCode = code == DBNull.Value ? 0 : Convert.ToInt32(code);
                if (errorCode != 0)
                {
                    var errorMessage = objectDb.GetParameterValue("ErrMsg").ToString();

                    throw new ThirdPartyProductException($"Error When Saving Time\nProductId: {param.ProductId}\n{errorCode} : {errorMessage}");
                }
            }
        }
    }
}
