using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.UnitTests.TestHelper
{
    public static class NSubtituteMockingHelper
    {
        public static IMarketingService FakeMarkengtingService()
        {
            var marketingService = Substitute.For<IMarketingService>();
            marketingService.GetTextList(Arg.Compat.Any<MarketingTextParam>()).Returns(new List<MarketingItem>
            {
                new MarketingItem { BannerId = 1, TextId = 1 }
            });
            marketingService.GetCategoryList(Arg.Compat.Any<bool>(), Arg.Compat.Any<bool>()).Returns(Constant.CategoryList);
            marketingService.GetLanguageList(Arg.Compat.Any<bool>()).Returns(Constant.LanguageTypeList);
            marketingService.GetCurrencyList(Arg.Compat.Any<bool>()).Returns(Constant.CurrencyType);
            marketingService.GetDomainList(Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(Constant.DomainList);
            marketingService.GetLandingPageList(Arg.Compat.Any<bool>()).Returns(Constant.LandingPageList);

            return marketingService;
        }

        public static ISessionService FakeSessionService()
        {
            var service = Substitute.For<ISessionService>();
            service.CaptchaCode.Returns(Constant.Captcha);
            service.CurrentLanguage.Returns(Constant.DefaultLanguage);
            service.IsInternal.Returns(false);
            service.UserSession.Returns(Constant.Session);
            return service;
        }

        public static ILanguageService FakeLanguageService()
        {
            var service = Substitute.For<ILanguageService>();
            service.GetFavoriteLanguage().Returns(Constant.DefaultLanguage);
            return service;
        }
    }
}
