using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class OfferService : IOfferService
    {
        private const string OFFER_CONTENT_PATH = "offerpage/getcontent";

        private readonly IOfferRepository _offerRepository;
        private readonly IHttpClientUtility _httpClientUtility;

        public OfferService(IHttpClientUtility httpClientUtility, IOfferRepository offerRepository)
        {
            _httpClientUtility = httpClientUtility;
            _offerRepository = offerRepository;
        }

        public async Task<string> GetOfferById(int id)
        {
            var item = _offerRepository.GetOfferById(id);

            if (item != null)
            {
                return await _httpClientUtility.GetContent(OFFER_CONTENT_PATH, item.FileName);
            }

            return string.Empty;
        }
    }
}
