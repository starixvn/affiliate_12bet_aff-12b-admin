using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class SpecificDomainControlService : BaseDynamicService, ISpecificDomainControlService
    {
        public SpecificDomainControlService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<SpecificDomainControlInfo> GetSpecificDomainControl(SpecificDomainControlParam param)
        {
            var criteria = param.MapTo<SpecificDomainControlParam, SpecificDomainControlCriteria>();

            //return FetchTData<SpecificDomainControlInfo>(criteria);

            return new List<SpecificDomainControlInfo> {
                new SpecificDomainControlInfo
                {
                    DomainControlId = 1,
                    AffName = "12BETAFF001",
                    AffDomain = "goto.12b.com",
                    AffDomainSSL = true,
                    RedirectDomain = "www.12b.com",
                    RedirectDomainSSL = true,
                    Creator = "AASa",
                    CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsUsed = true
                },
                new SpecificDomainControlInfo
                {
                    DomainControlId = 2,
                    AffName = "12BETAFF002",
                    AffDomain = "goto.12b.com",
                    AffDomainSSL = true,
                    RedirectDomain = "www.12b.com",
                    RedirectDomainSSL = true,
                    Creator = "AASa",
                    CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsUsed = true,
                    Editor = "AACamy",
                    UpdatedDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                }
            };
        }

        public SpecificDomainControlInfo GetSpecificDomainControlById(int domainControlId)
        {
            //var criteria = new SpecificDomainControlCriteria();

            //return GetTData<SpecificDomainControlInfo>(criteria);
            return new SpecificDomainControlInfo
            {
                DomainControlId = 1,
                AffName = "12BETAFF001",
                AffDomain = "goto.12b.com",
                AffDomainSSL = true,
                RedirectDomain = "www.12b.com",
                RedirectDomainSSL = true,
                Creator = "AASa",
                CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsUsed = true
            };
        }

        public ResultInfo InsertSpecificDomainControl(InsertSpecificDomainControlParam param)
        {
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
            command.AffName = param.AffName;
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
