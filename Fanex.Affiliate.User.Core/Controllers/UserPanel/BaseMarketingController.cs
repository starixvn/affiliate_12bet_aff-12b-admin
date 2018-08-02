using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    public class BaseMarketingController : Controller
    {
        protected readonly ISessionService _sessionService;
        protected readonly IAppSetting _appSetting;
        protected readonly IMarketingService _marketingService;
        protected readonly IAccountRepository _accountRepository;

        public BaseMarketingController(ISessionService sessionService, IAppSetting appSetting,
            IMarketingService marketingService,
            IAccountRepository accountRepository)
        {
            _sessionService = sessionService;
            _appSetting = appSetting;
            _marketingService = marketingService;
            _accountRepository = accountRepository;
        }
        protected SearchFormViewModel GetSearchFormViewModel(bool isMediaPage)
        {
            var bannerTypeList = _marketingService.GetBannerTypeList(_sessionService.IsInternal);

            var categoryType = isMediaPage ? BannerTypeName.CategoryMedia : BannerTypeName.Category;
            var viewModel = new SearchFormViewModel
            {
                CategoryList = GetDataByType(bannerTypeList, categoryType),
                LanguageList = GetLanguageList(bannerTypeList),
                CurrencyList = GetDataByType(bannerTypeList, BannerTypeName.Currency)
            };

            if (isMediaPage)
            {
                viewModel.ImageTypeList = GetDataByType(bannerTypeList, BannerTypeName.ImageType);
                viewModel.SizeList = GetDataByType(bannerTypeList, BannerTypeName.Size);
            }

            return viewModel;
        }

        protected LinkFormViewModel GetLinkFormViewModel()
        {
            var userSession = _sessionService.UserSession;
            var isInternal = _sessionService.IsInternal;

            var userHasCasino = _accountRepository.UserHasCasino(userSession.UserName, isInternal);
            var memberSitePage = new MemberSitePage(userHasCasino);

            var viewModel = new LinkFormViewModel
            {
                DomainList = _marketingService.GetDomainList(userSession.AffId, isInternal),
                MemberSitePageList = memberSitePage.PageList.Select(p => new SelectListItem { Value = p, Text = p })
            };

            return viewModel;
        }

        protected IEnumerable<SelectListItem> GetLanguageList(IEnumerable<BannerType> bannerTypeList)
        {
            var languageByType = bannerTypeList.Where(t => t.Type.MatchWith(BannerTypeName.Language));

            var dataList = new List<SelectListItem>();
            foreach (var item in languageByType)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(item.Value));
                if (language != null)
                {
                    dataList.Add(new SelectListItem
                    {
                        Value = language.Code,
                        Text = SystemLanguage.GetLanguageText(item.Value),
                        Selected = language.Code.MatchWith(_sessionService.UserSession.PreferredLanguage)
                    });
                }
            }

            return dataList;
        }

        protected IEnumerable<SelectListItem> GetDataByType(IEnumerable<BannerType> bannerTypeList, string type)
        {
            return bannerTypeList.Where(p => p.Type.MatchWith(type))
                .Select(i => new SelectListItem { Value = i.Value, Text = SystemLanguage.GetDisplayText(i.Value) });
        }

        protected IDictionary<string, string> GetTextLinkList(string selectedLanguage, string selectedPage,
            Domain selectedDomain, IEnumerable<SelectListItem> languageList)
        {
            if (selectedDomain == null)
            {
                return new Dictionary<string, string>();
            }

            var userSession = _sessionService.UserSession;

            var linkGenerationParam = new LinkGenerationParam
            {
                AffId = userSession.AffId,
                TextLinkTemplate = _appSetting.TextLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedLanguage = selectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                LanguageList = languageList
            };

            return _marketingService.GenerateLinkList(linkGenerationParam);
        }
    }
}
