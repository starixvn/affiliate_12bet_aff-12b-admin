using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class CommonSettingService : ICommonSettingService
    {
        private readonly ICommonSettingRepository _commonSettingRepository;
        private readonly ISessionService _sessionService;

        public CommonSettingService(ICommonSettingRepository commonSettingRepository, ISessionService sessionService)
        {
            _commonSettingRepository = commonSettingRepository;
            _sessionService = sessionService;
        }

        public IEnumerable<Language> GetInChangeMarkets()
        {
            var languages = _commonSettingRepository.GetLanguageList();
            var inChangeMarket = _sessionService.UserClaim.FindAll(Constants.Function.InChargeMarket).Select(c => c.Value);
            return languages.Where(l => inChangeMarket.Contains(l.LanguageCode));
        }

        public IEnumerable<Product> GetProductList()
        {
            return _commonSettingRepository.GetProductList();
        }

        public IEnumerable<AffType> GetAffTypeList()
        {
            return _commonSettingRepository.GetAffTypeList();
        }

        public IEnumerable<Country> GetCountryList()
        {
            return _commonSettingRepository.GetCountryList();
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
