using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class PlayerDetailService : BaseDynamicService, IPlayerDetailService
    {
        public PlayerDetailService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<PlayerDetail> GetPlayerDetailList(PlayerDetailParam param)
        {
            var criteria = new PlayerDetailCriteria
            {
                AffId = param.AffId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                ProductId = param.ProductId,
                MonthDate = param.MonthDate,
                IsSortAsc = param.IsSortAsc,
                IsExportExcel = param.IsExportExcel,
                SortById = param.SortById,
                BaseCurrencyId = param.BaseCurrencyId,
                CurrencyIds = param.CurrencyIds,
                IsIncludeRemovedPlayer = param.IsIncludeRemovedPlayer
            };

            return FetchEntityResult<PlayerDetail>(criteria);
        }
    }
}
