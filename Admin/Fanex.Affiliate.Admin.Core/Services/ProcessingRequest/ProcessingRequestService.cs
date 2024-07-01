using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Repository.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Services.ProcessingRequest
{
    public class ProcessingRequestService : IProcessingRequestService
    {
        private readonly ISessionService _sessionService;
        private readonly IProcessingRequestRepository _repo;

        public ProcessingRequestService(ISessionService sessionService, IProcessingRequestRepository repository)
        {
            _sessionService = sessionService;
            _repo = repository;
        }

        public ResultInfo AddProcessingRequest(ProcessingRequestInsParam param)
        {
            param.Ip = _sessionService.RequestIp;
            param.UserId = _sessionService.UserId;
            return _repo.AddProcessingRequest(param);
        }

        public ResultInfo DeleteProcessingRequest(ProcessingRequestDelParam param)
        {
            param.Ip = _sessionService.RequestIp;
            param.UserId = _sessionService.UserId;
            return _repo.DeleteProcessingRequest(param);
        }

        public ProcessingRequestDetailInfo GetProcessingRequestDetail(int processingRequestId)
            => _repo.GetProcessingRequestDetail(processingRequestId);

        public ProcessingRequestEntity GetProcessingRequestList(ProcessingRequestSelParam param)
            => _repo.GetProcessingRequestList(param);

        public ResultInfo UpdateProcessingRequest(ProcessingRequestUpdParam param)
        {
            param.Ip = _sessionService.RequestIp;
            param.UserId = _sessionService.UserId;
            return _repo.UpdateProcessingRequest(param);
        }
    }
}
