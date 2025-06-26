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
                AffId = param.AffId,
                UserId = param.UserId,
                Ip = param.Ip,
                Status = param.Status,
                Password = param.Password,
            };

            var result = ExecuteReturnResultInfo(command);

            return (result, command.AffCode);
        }

        public EntityResult<AffiliateInfo> GetAffiliateList(AffiliateInfoParam param)
        {
            var criteria = new AffiliateCriteria
            {
                AffCode = param.AffCode,
                UserId = param.UserId,
                GroupIds = param.GroupIds,
                TargetCountryCode = param.TargetCountryCode,
                PreferredLanguage = param.Language,
                Status = param.Status,
                ParentAffName = param.ParentAffName,
                UserName = param.UserName,
                Player_UserName = param.AffPlayer,
                Email = param.Email,
                SortType = param.SortBy,
                SortDirectionType = param.SortDirectionType,
                IsExportExcel = param.IsExportExcel,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                IsExcludeMarketingAccount = param.IsExcludeMarketingAccount
            };

            return FetchEntityResult<AffiliateInfo>(criteria);
        }

        public ResultInfo ToggleFavorite(FavoriteParam param)
        {
            var command = new ToggleFavoriteCommand
            {
                AffId = param.AffId,
                UserId = param.UserId,
                Ip = param.Ip,
                IsFavorite = param.IsFavorite,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo ChangePassword(PasswordParam param)
        {
            var command = new ChangeAffPasswordCommand
            {
                AffId = param.AffId,
                UserId = param.UserId,
                Ip = param.Ip,
                Password = param.Password,
            };

            return ExecuteReturnResultInfo(command);
        }
    }
}
