using System;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.Core.Constants;

namespace Fanex.Affiliate.DataService.Job.Repository
{
    public class TimeTrackerRepository : BaseRepository, ITimeTrackerRepository
    {
        public DateTime? GetLastTimeCallSuccess(TimeTrackerBaseParam param)
        {
            const string SpKey = "Task_APIProduct_CallTimeByProductSel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("ProductId", param.ProductId);
                objectDb.SetParameterValue("Currency", param.Currency);

                return (DateTime?)objectDb.ExecuteScalar();
            }
        }

        public void SaveTime(InsertTimeTrackerParam param)
        {
            const string SpKey = "Task_APIProduct_CallTimeIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                var query = new Dictionary<object, object>();
                query.Add("ProductId", param.ProductId);
                query.Add("Currency", param.Currency);
                query.Add("CallTime", param.CallTime);
                query.Add("ErrCode", RespositoryResponse.Success);
                query.Add("ErrMsg", RespositoryResponse.SuccessMessage);

                objectDb.ExecuteNonQuery(query);

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
