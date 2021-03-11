using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class NewsLetterService : INewsLetterService
    {
        private const string NEWSLETTER_CONTENT_PATH = "newsletter/getcontent";

        private static Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>();
        private static HttpClient HttpClient => LazyHttpClient.Value;
        private INewsLetterRepository Repository { get; }
        private ISessionService SessionService { get; }
        private IAppSetting AppSetting { get; }

        public NewsLetterService(INewsLetterRepository repository, ISessionService sessionService, IAppSetting appSetting)
        {
            Repository = repository;
            SessionService = sessionService;
            AppSetting = appSetting;
        }

        public async Task<string> GetContentById(int id)
        {
            var item = Repository.GetNewsLetterById(id);
            if (item != null)
            {
                return await GetContent(item.FileName);
            }
            return string.Empty;
        }

        public async Task<string> GetCurrentContent()
        {
            var items = Repository.GetNewsLettersByDate(DateTime.Now)
                .OrderByDescending(n => n.UploadDate);
            var currentLang = SessionService.CurrentLanguage;
            var item = items.FirstOrDefault(i => string.Equals(currentLang, i.Language, StringComparison.OrdinalIgnoreCase))
                ?? items.FirstOrDefault(i => string.Equals(SystemLanguage.DefaultLanguageCode, i.Language, StringComparison.OrdinalIgnoreCase))
                ?? items.FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }

            return await GetContent(item.FileName);
        }

        public bool HasNewCurrentTime()
        {
            var items = Repository.GetNewsLettersByDate(DateTime.Now);
            return items.Any();
        }

        private async Task<string> GetContent(string fileName)
        {
            var url = $"{AppSetting.ResourceDomain}{NEWSLETTER_CONTENT_PATH}?fileName={fileName}";
            var content = await HttpClient.GetAsync(url);
            if (content.IsSuccessStatusCode)
            {
                return await content.Content.ReadAsStringAsync();
            }
            else
            {
                await Logger.Log.ErrorAsync($"Request to take Newsletters {fileName} content fail");
            }
            return string.Empty;
        }
    }
}
