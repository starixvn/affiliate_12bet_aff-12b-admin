using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : BaseDynamicService, IDomainControlService
    {
        public DomainControlService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<DomainControlInfo> GetDomainControlList(string market)
        {
            var criteria = new DomainControlCriteria
            {
                MarketId = market,
            };

            return FetchTData<DomainControlInfo>(criteria);
        }

        public ResultInfo InsertDomainControl(InsertUpdateDomainControlParam param)
        {
            var command = new InsertDomainControlCommand();
            AssignBaseDomainControlCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateDomainControl(InsertUpdateDomainControlParam param)
        {
            var command = new UpdateDomainControlCommand
            {
                DomainControlId = param.DomainId,
            };
            AssignBaseDomainControlCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            var command = new DeleteDomainControlCommand
            {
                DomainControlId = param.DomainId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignBaseDomainControlCommand(BaseDomainControlCommand command, InsertUpdateDomainControlParam param)
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
