using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class SystemService : ISystemService
    {
        private readonly ISystemRepository _systemService;
        private readonly ISessionService _sessionService;

        public SystemService(ISystemRepository systemService, ISessionService sessionService)
        {
            _systemService = systemService;
            _sessionService = sessionService;
        }

        public IEnumerable<Language> GetInChangeMarkets()
        {
            return GetAllInChangeMarkets().Where(l => !l.LanguageCode.Equals("hi") && !l.LanguageCode.Equals("te-IN"));
        }

        public IEnumerable<Language> GetAllInChangeMarkets()
        {
            var languages = _systemService.GetLanguageList();
            var inChangeMarket = _sessionService.UserClaim.FindAll(Constants.Function.InChargeMarket).Select(c => c.Value);
            return languages.Where(l => inChangeMarket.Contains(l.LanguageCode));
        }

        public IEnumerable<Product> GetProductList()
        {
            return _systemService.GetProductList();
        }

        public IEnumerable<AffType> GetAffTypeList()
        {
            return _systemService.GetAffTypeList();
        }

        public IEnumerable<Country> GetCountryList()
        {
            return _systemService.GetCountryList();
        }

        public IEnumerable<TargetCountry> GetTargetCountryListByUser()
        {
            return _systemService.GetTargetCountryList(_sessionService.UserId);
        }

        public IEnumerable<Language> GetTargetLanguageListByUser()
        {
            return GetAllTargetLanguageListByUser().Where(l => !l.LanguageCode.Equals("hi") && !l.LanguageCode.Equals("te-IN"));
        }

        public IEnumerable<Language> GetAllTargetLanguageListByUser()
        {
            return _systemService.GetTargetLanguageList(_sessionService.UserId);
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
