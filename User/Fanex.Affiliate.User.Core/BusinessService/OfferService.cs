namespace Fanex.Affiliate.User.Core.BusinessService
{
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using System.Threading.Tasks;
    using Utilities;

    public class OfferService : BaseDynamicService, IOfferService
    {
        private const string OFFER_CONTENT_PATH = "offerpage/getcontent";

        private readonly IHttpClientUtility _httpClientUtility;

        public OfferService(
            IDynamicRepository dynamicRepository,
            IHttpClientUtility httpClientUtility)
            : base(dynamicRepository)
        {
            _httpClientUtility = httpClientUtility;
        }

        public async Task<string> GetOfferById(int id)
        {
            var item = GetTData<Offer>(new GetOfferByIdCriteria(id));

            if (item != null)
            {
                return await _httpClientUtility.GetContent(OFFER_CONTENT_PATH, item.FileName);
            }

            return string.Empty;
        }
    }
}
