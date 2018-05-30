using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IHotNewsService
    {
        LatestHotNews GetLatestHotNews(bool isInternal);
    }
}
