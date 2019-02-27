using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IPlayerDetailRepository
    {
        PlayerDetailResult GetPlayerDetailList(PlayerDetailParam param);
    }
}
