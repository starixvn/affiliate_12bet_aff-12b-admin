using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public AffiliateService(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }
        public AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null)
        {
            var criteria = new AffBasicInfoCriteria
            {
                AffId = affId,
                AffName = affName
            };

            return _dynamicRepository.Get<AffiliateBasicInfo>(criteria);
        }

        public (ResultInfo, string) ApproveAffiliate(ApproveAffiliateParam param)
        {
            var command = new ApproveAffCommand
            {
                AffId = param.AffId,
                Status = param.Status,
                Password = param.Password,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            _dynamicRepository.Get<AffiliateBasicInfo>(command);

            return (new ResultInfo(command.ErrCode, command.ErrMsg), command.AffCode);
        }
    }
}
