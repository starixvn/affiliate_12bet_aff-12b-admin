using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using System;
using System.Linq;
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
            var item = items.FirstOrDefault(i => i.Language.MatchWith(currentLang))
                ?? items.FirstOrDefault(i => i.Language.MatchWith(SystemLanguage.DefaultLanguageCode))
                ?? items.FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }

            return await HttpClientUtility.GetContent(NEWSLETTER_CONTENT_PATH, item.FileName);
        }
    }
}
