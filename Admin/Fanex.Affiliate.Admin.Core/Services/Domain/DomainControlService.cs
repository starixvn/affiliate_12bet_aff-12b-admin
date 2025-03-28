using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : BaseDynamicService, IDomainControlService
    {
        private readonly ICommonDomainService _commonDomainService;

        public DomainControlService(
            IDynamicRepository dynamicRepository,
            ICommonDomainService commonDomainService) : base(dynamicRepository)
        {
            _commonDomainService = commonDomainService;
        }

        public IEnumerable<Market> GetMarketListByDomain(int affDomainId = 0, int redirectDomainId = 0)
            => _commonDomainService.GetMarketListByDomain(affDomainId, redirectDomainId);

        public IEnumerable<DomainControlInfo> GetDomainControl(string market, int domainControlId = 0)
        {
            var criteria = new DomainControlCriteria
            {
                MarketId = market,
                DomainControlId = domainControlId
            };

            return FetchTData<DomainControlInfo>(criteria);
        }

        public DomainControlInfo GetDomainControlById(int domainControlId)
        {
            return GetDomainControl(string.Empty, domainControlId).FirstOrDefault();
        }

        public IEnumerable<string> GetSubAffDomainControlList()
        {
            var subDomainInfos = FetchTData<AffSubDomainInfo>(new DomainControlCriteria());

            return subDomainInfos.Select(s => s.AffSubDomain)
                .Where(s => !string.IsNullOrEmpty(s)).Distinct();
        }

        public ResultInfo InsertDomainControl(InsertDomainControlParam param)
        {
            var command = new InsertDomainControlCommand();
            AssignBaseDomainControlCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateDomainControl(UpdateDomainControlParam param)
        {
            var command = new UpdateDomainControlCommand
            {
                DomainControlId = param.DomainControlId,
            };
            AssignBaseDomainControlCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            var command = new DeleteDomainControlCommand
            {
                DomainControlId = param.DomainControlId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignBaseDomainControlCommand(BaseDomainControlCommand command, InsertDomainControlParam param)
        {
            command.AffDomainID = param.AffDomainId;
            command.LicenseeDomainId = param.RedirectDomainId;
            command.MarketId = param.MarketId;
            command.Status = param.IsUsed;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}
