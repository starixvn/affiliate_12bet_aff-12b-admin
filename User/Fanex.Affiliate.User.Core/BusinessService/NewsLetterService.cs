namespace Fanex.Affiliate.User.Core.BusinessService
{
    using Constants;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using Extensions;
    using System.Linq;
    using System.Threading.Tasks;
    using Utilities;

    public class NewsLetterService : BaseDynamicService, INewsLetterService
    {
        private const string NEWSLETTER_CONTENT_PATH = "newsletter/getcontent";

        private ISessionService SessionService { get; }
        private IHttpClientUtility HttpClientUtility { get; }

        public NewsLetterService(
            IDynamicRepository dynamicRepository,
            ISessionService sessionService,
            IHttpClientUtility httpClientUtility) : base(dynamicRepository)
        {
            SessionService = sessionService;
            HttpClientUtility = httpClientUtility;
        }

        public async Task<string> GetContentById(int id)
        {
            var item = GetTData<NewsLetter>(new GetNewsLetterByIdCriteria(id));
            if (item != null)
            {
                return await HttpClientUtility.GetContent(NEWSLETTER_CONTENT_PATH, item.FileName);
            }
            return string.Empty;
        }

        public async Task<string> GetCurrentContent()
        {
            var items = FetchTData<NewsLetter>(new GetNewsLettersByDateCriteria(DateTimeHelper.Gmt8Now))
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
