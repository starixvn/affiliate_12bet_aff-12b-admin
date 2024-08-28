using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;

namespace Fanex.Affiliate.Admin.Core.Services.ProcessingRequest
{
    public interface IProcessingRequestService
    {
        ProcessingRequestEntity GetProcessingRequestList(ProcessingRequestSelParam param);

        ResultInfo AddProcessingRequest(ProcessingRequestInsParam param);

        ResultInfo UpdateProcessingRequest(ProcessingRequestUpdParam param);

        ResultInfo DeleteProcessingRequest(ProcessingRequestDelParam param);

        ProcessingRequestDetailInfo GetProcessingRequestDetail(int processingRequestId);
    }
}
