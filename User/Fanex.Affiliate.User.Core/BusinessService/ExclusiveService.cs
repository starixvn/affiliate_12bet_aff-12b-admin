using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class ExclusiveService : IExclusiveService
    {
        private const string EXCLUSIVE_CONTENT_PATH = "exclusive/getcontent";
        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>();
        private static readonly HttpClient httpClient = LazyHttpClient.Value;

        private readonly IExclusiveRepository _exclusiveRepository;
        private readonly IAppSetting _appSetting;

        public ExclusiveService(IAppSetting appSetting, IExclusiveRepository exclusiveRepository)
        {
            _appSetting = appSetting;
            _exclusiveRepository = exclusiveRepository;
        }

        public async Task<string> GetExclusiveById(int id)
        {
            var item = _exclusiveRepository.GetExclusiveById(id);

            if (item != null)
            {
                return await GetContent(item.FileName);
            }

            return string.Empty;
        }

        private async Task<string> GetContent(string fileName)
        {
            var url = new Uri($"{_appSetting.ResourceDomain}{EXCLUSIVE_CONTENT_PATH}?fileName={fileName}");
            var content = await httpClient.GetAsync(url);
            if (content.IsSuccessStatusCode)
            {
                return await content.Content.ReadAsStringAsync();
            }
            else
            {
                await Logger.Log.ErrorAsync($"Request to take Exclusive {fileName} content is failed!");
            }
            return string.Empty;
        }
    }
}
