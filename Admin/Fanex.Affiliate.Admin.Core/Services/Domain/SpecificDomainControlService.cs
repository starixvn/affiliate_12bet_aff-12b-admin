using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class SpecificDomainControlService : BaseDynamicService, ISpecificDomainControlService
    {
        private readonly int MAXIMUM_AFFILIATES_NUMBER = 500;

        public SpecificDomainControlService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public EntityResult<SpecificDomainControlInfo> GetSpecificDomainControl(SpecificDomainControlParam param)
        {
            var criteria = param.MapTo<SpecificDomainControlParam, SpecificDomainControlCriteria>();

            return FetchEntityResult<SpecificDomainControlInfo>(criteria);
        }

        public SpecificDomainControlInfo GetSpecificDomainControlById(int id)
        {
            var specificDomains = FetchTData<SpecificDomainControlInfo>(new SpecificDomainControlCriteria());

            return specificDomains.FirstOrDefault(s => s.SpecificDomainControlId == id);
        }

        public ResultInfo InsertSpecificDomainControl(InsertSpecificDomainControlParam param)
        {
            if (param.AffUsername.Split(',').Length > MAXIMUM_AFFILIATES_NUMBER)
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
                SpecificDomainControlId = param.SpecificDomainControlId,
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
            command.AffUsername = param.AffUsername;
            command.AffDomain = param.AffDomain;
            command.AffDomainIsSSL = param.AffDomainIsSSL;
            command.RedirectDomain = param.RedirectDomain;
            command.RedirectDomainIsSSL = param.RedirectDomainIsSSL;
            command.Status = param.Status;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}
