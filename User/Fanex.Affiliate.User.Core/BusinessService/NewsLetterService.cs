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
        private INewsLetterRepository Repository { get; }
        private ISessionService SessionService { get; }
        private IHttpClientUtility HttpClientUtility { get; }

        public NewsLetterService(INewsLetterRepository repository, ISessionService sessionService, IHttpClientUtility httpClientUtility)
        {
            Repository = repository;
            SessionService = sessionService;
            HttpClientUtility = httpClientUtility;
        }

        public async Task<string> GetContentById(int id)
        {
            var item = Repository.GetNewsLetterById(id);
            if (item != null)
            {
                return await HttpClientUtility.GetContent(NEWSLETTER_CONTENT_PATH, item.FileName);
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

            return await HttpClientUtility.GetContent(NEWSLETTER_CONTENT_PATH, item.FileName);
        }

        public bool HasNewCurrentTime()
        {
            var items = Repository.GetNewsLettersByDate(DateTime.Now);
            return items.Any();
        }
    }
}
