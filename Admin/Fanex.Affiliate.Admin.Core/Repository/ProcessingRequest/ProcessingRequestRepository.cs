using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Repository.ProcessingRequest
{
    public class ProcessingRequestRepository : BaseRepository, IProcessingRequestRepository
    {
        public ResultInfo AddProcessingRequest(ProcessingRequestInsParam param)
        {
            const string spKey = "Aff_Admin_ProcessingRequest_Ins";

            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteProcessingRequest(ProcessingRequestDelParam param)
        {
            const string spKey = "Aff_Admin_ProcessingRequest_Del";

            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }


        public ProcessingRequestDetailInfo GetProcessingRequestDetail(int processingRequestId)
        {
            const string spKey = "Aff_Admin_ProcessingRequestDetail_Sel";

            using (var objectDb = GetObjectDb(spKey))
            {
                return objectDb.QueryEntity<ProcessingRequestDetailInfo>(new { RequestId = processingRequestId });
            }
        }

        public ProcessingRequestEntity GetProcessingRequestList(ProcessingRequestSelParam param)
        {
            const string spKey = "Aff_Admin_ProcessingRequest_Sel";

            var result = new ProcessingRequestEntity();

            using (var objectDb = GetObjectDb(spKey))
            {
                using (var reader = objectDb.QueryMultiple(param))
                {
                    result.MainData = reader.Read<ProcessingRequestBasicInfo>();
                    result.Summary = reader.Read<ProcessingRequestSummaryInfo>().First();
                    result.TotalItem = GetTotalRow(objectDb);
                }
            }
            return result;
        }

        public ResultInfo UpdateProcessingRequest(ProcessingRequestUpdParam param)
        {
            const string spKey = "Aff_Admin_ProcessingRequest_Upd";

            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }
    }
}
