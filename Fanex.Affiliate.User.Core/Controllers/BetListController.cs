using System.Text.RegularExpressions;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BetListWebService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels.BetList;
using Fanex.Affiliate.User.Core.Repository.BetList;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.BetList;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [Authenticate]
    public class BetListController : BaseController
    {
        private static readonly Product[] supportedProducts = new Product[2]
        {
            new Product { ProductId = 1, ProductName = "Sportsbook" },
            new Product { ProductId = 5, ProductName = "Playtech Casino" }
        };

        private readonly IBetListRepository _betlistRepository;

        public BetListController(ISessionService sessionService, IAppSetting appSetting, IBetListRepository betlistRepository)
            : base(sessionService, appSetting)
        {
            _betlistRepository = betlistRepository;
        }

        [HttpGet]
        public ActionResult Index(BetListParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;

            var isValidMember = IsValidMember(param.UserName);

            if (isValidMember)
            {
                var affBetlistService = new AffiliateAPISoapClient();
                var betlistSOAP = new BetlistSOAP
                {
                    AffiliateName = "12bet",
                    Username = Regex.Replace(param.UserName, @"12[Bb]et\$", string.Empty),
                    Product = "Sportsbook",
                    FromDate = param.Date,
                    ToDate = param.Date,
                    ExchangeRate = 1,
                    SupportedProducts = supportedProducts,
                    Language = SessionService.CurrentLanguage
                };

                var viewModel = new MainBetListViewModel
                {
                    BetListHtml = affBetlistService.GetBetlist(betlistSOAP),
                    SelectedDate = param.Date,
                    SelectedSportType = param.SportType,
                    UserName = param.UserName
                };

                return View(viewModel);
            }

            return View(new MainBetListViewModel());
        }

        private bool IsValidMember(string userName)
        {
            var validationParam = new BetListValidationParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                MemberName = userName
            };

            return _betlistRepository.IsValidMember(validationParam);
        }
    }
}