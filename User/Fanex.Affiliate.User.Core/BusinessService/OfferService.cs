using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class OfferService : IOfferService
    {
        private const string OFFER_CONTENT_PATH = "offerpage/getcontent";
        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>();
        private static readonly HttpClient httpClient = LazyHttpClient.Value;

        private readonly IOfferRepository _offerRepository;
        private readonly IAppSetting _appSetting;

        public OfferService(IAppSetting appSetting, IOfferRepository offerRepository)
        {
            _appSetting = appSetting;
            _offerRepository = offerRepository;
        }

        public async Task<string> GetOfferById(int id)
        {
            var item = _offerRepository.GetOfferById(id);

            if (item != null)
            {
                return await GetContent(item.FileName);
            }

            return string.Empty;
        }

        private async Task<string> GetContent(string fileName)
        {
            var url = new Uri($"{_appSetting.ResourceDomain}{OFFER_CONTENT_PATH}?fileName={fileName}");
            var content = await httpClient.GetAsync(url);
            if (content.IsSuccessStatusCode)
            {
                return await content.Content.ReadAsStringAsync();
            }
            else
            {
                await Logger.Log.ErrorAsync($"Request to take Offer {fileName} content is failed!");
            }
            return string.Empty;
        }
    }
}
