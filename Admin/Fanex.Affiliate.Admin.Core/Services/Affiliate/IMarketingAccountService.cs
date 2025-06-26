using System.Threading.Tasks;
using System.Web;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public interface IMarketingAccountService
    {
        MarketingAccount GetMarketingAccountInfo(int affId, HttpServerUtilityBase server);

        Task<ResultInfo> CreateMarketingAccountAsync(CreateMarketingAccountModel model, HttpServerUtilityBase server);
    }
}
