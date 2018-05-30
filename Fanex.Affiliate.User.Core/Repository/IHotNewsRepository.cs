using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IHotNewsRepository
    {
        LatestHotNews GetLastestHotNews(bool isInternal);
    }
}
