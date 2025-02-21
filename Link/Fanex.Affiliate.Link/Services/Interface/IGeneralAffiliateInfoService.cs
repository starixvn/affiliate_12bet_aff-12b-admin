using Fanex.Affiliate.Link.DTOs;

namespace Fanex.Affiliate.Link.Services.Interface
{
    public interface IGeneralAffiliateInfoService
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        AffiliateInfo GetGeneralAffiliateInfo(int? affId = null, string affCode = null);

        CommGroup GetCommGroupByAffCode(string affCode);
    }
}
