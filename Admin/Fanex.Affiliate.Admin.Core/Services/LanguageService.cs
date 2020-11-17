using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ISessionService _sessionService;

        public LanguageService(ILanguageRepository languageRepository, ISessionService sessionService)
        {
            _languageRepository = languageRepository;
            _sessionService = sessionService;
        }

        public IEnumerable<Language> GetLanguageList()
        {
            return _languageRepository.GetLanguageList();
        }

        public IEnumerable<Language> GetInChangeMarkets()
        {
            var languages = _languageRepository.GetLanguageList();
            var inChangeMarket = _sessionService.UserClaim.FindAll(Function.InChargeMarket).Select(c => c.Value);
            return languages.Where(l => inChangeMarket.Contains(l.LanguageCode));
        }

        public void SetCulture(string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        }
    }
}
