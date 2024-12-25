using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class CommonDomainService : BaseDynamicService, ICommonDomainService
    {
        public CommonDomainService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<DomainInfo> GetDomainList(string spKey)
        {
            return FetchTData<DomainInfo>(spKey);
        }

        public ResultInfo InsertDomain(InsertUpdateDomainParam param, string spKey)
        {
            var command = new InsertDomainCommand(spKey);
            AssignDomainCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateDomain(InsertUpdateDomainParam param, string spKey)
        {
            var command = new UpdateDomainCommand(spKey)
            {
                DomainId = param.DomainId,
            };
            AssignDomainCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param, string spKey)
        {
            var command = new DeleteDomainCommand(spKey)
            {
                DomainId = param.DomainId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignDomainCommand(InsertDomainCommand command, InsertUpdateDomainParam param)
        {
            command.DomainName = param.DomainName;
            command.IsSSL = param.IsSSL;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}
