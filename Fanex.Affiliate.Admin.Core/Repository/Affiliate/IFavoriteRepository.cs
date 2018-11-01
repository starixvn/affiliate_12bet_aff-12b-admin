using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public interface IFavoriteRepository
    {
        ResultInfo UpdateFavorite(InsertUpdateFavoriteParam param);
    }
}
