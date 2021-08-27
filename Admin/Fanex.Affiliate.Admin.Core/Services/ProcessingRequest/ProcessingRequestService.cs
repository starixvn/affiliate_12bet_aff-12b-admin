using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Repository.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Net;

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
            var encodeParam = EncodeParam(param);
            encodeParam.UserName = param.UserName;
            return _repo.AddProcessingRequest(encodeParam);
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
            var encodeParam = EncodeParam(param);
            encodeParam.RequestId = param.RequestId;
            return _repo.UpdateProcessingRequest(encodeParam);
        }

        private  T EncodeParam<T>(T param)
            where T : ProcessingRequestBaseParam, new()
        {
            return new T
            {
                AgentName = WebUtility.HtmlEncode(param.AgentName),
                BankAccountName = WebUtility.HtmlEncode(param.BankAccountName),
                BankAccountNo = WebUtility.HtmlEncode(param.BankAccountNo),
                BankAddress = WebUtility.HtmlEncode(param.BankAddress),
                BankName = WebUtility.HtmlEncode(param.BankName),
                BankSwiftCode = WebUtility.HtmlEncode(param.BankSwiftCode),
                ContactModeTime = WebUtility.HtmlEncode(param.ContactModeTime),
                DeadLine = param.DeadLine,
                Ip = param.Ip,
                Language = param.Language,
                MarketId = param.MarketId,
                Player_Username = param.Player_Username,
                PreferredPayment = param.PreferredPayment,
                PromotionMethod = WebUtility.HtmlEncode(param.PromotionMethod),
                Remark = WebUtility.HtmlEncode(param.Remark),
                ServerCountry = WebUtility.HtmlEncode(param.ServerCountry),
                Status = param.Status,
                TargetMarket = WebUtility.HtmlEncode(param.TargetMarket),
                UserId = param.UserId,
                WebAdvAff = WebUtility.HtmlEncode(param.WebAdvAff),
                WebRank = WebUtility.HtmlEncode(param.WebRank),
                WebRegDate = WebUtility.HtmlEncode(param.WebRegDate),
                WebSMLanguage = WebUtility.HtmlEncode(param.WebSMLanguage),
                WebSMQuality = WebUtility.HtmlEncode(param.WebSMQuality),
                WebSMType = WebUtility.HtmlEncode(param.WebSMType),
            };
        }
    }
}
