using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class AffiliateService : BaseDynamicService, IAffiliateService
    {
        public AffiliateService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }
        public AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null)
        {
            var criteria = new AffBasicInfoCriteria
            {
                AffId = affId,
                AffName = affName
            };

            return GetTData<AffiliateBasicInfo>(criteria);
        }

        public (ResultInfo, string) ApproveAffiliate(ApproveAffiliateParam param)
        {
            var command = new ApproveAffCommand
            {
                Status = param.Status,
                Password = param.Password,
            };

            var result = ExecuteAffReturnResultInfo(command, param);

            return (result, command.AffCode);
        }
    }
}
