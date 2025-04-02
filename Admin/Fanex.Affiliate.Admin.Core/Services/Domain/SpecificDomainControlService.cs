using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class SpecificDomainControlService : BaseDynamicService, ISpecificDomainControlService
    {
        private readonly int MAXIMUM_AFFILIATES_NUMBER = 500;

        public SpecificDomainControlService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<SpecificDomainControlInfo> GetSpecificDomainControl(SpecificDomainControlParam param)
        {
            var criteria = param.MapTo<SpecificDomainControlParam, SpecificDomainControlCriteria>();

            return FetchTData<SpecificDomainControlInfo>(criteria);
        }

        public SpecificDomainControlInfo GetSpecificDomainControlById(int id)
        {
            var criteria = new DomainControlCriteria
            {
                DomainControlId = id
            };

            return GetTData<SpecificDomainControlInfo>(criteria);
        }

        public ResultInfo InsertSpecificDomainControl(InsertSpecificDomainControlParam param)
        {
            if (param.AffNames.Split(',').Length > MAXIMUM_AFFILIATES_NUMBER)
            {
                return ResultInfo.GenerateErr("The function can only create assignment for up to 500 affiliates per request!");
            }

            var command = new InsertSpecificDomainControlCommand();
            AssignBaseDomainControlCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateSpecificDomainControl(UpdateSpecificDomainControlParam param)
        {
            var command = new UpdateSpecificDomainControlCommand
            {
                DomainControlId = param.DomainControlId,
            };
            AssignBaseDomainControlCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteSpecificDomainControl(DeleteDomainControlParam param)
        {
            var command = new DeleteDomainControlCommand
            {
                DomainControlId = param.DomainControlId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignBaseDomainControlCommand(InsertSpecificDomainControlCommand command, InsertSpecificDomainControlParam param)
        {
            command.AffNames = param.AffNames;
            command.AffDomain = param.AffDomain;
            command.AffDomainSSL = param.AffDomainSSL;
            command.RedirectDomain = param.RedirectDomain;
            command.RedirectDomainSSL = param.RedirectDomainSSL;
            command.IsUsed = param.IsUsed;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}
