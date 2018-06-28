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
        private const string AffiliateName = "12bet";
        private const string Language = "en-US";
        private const string ExchangeRate = "1";

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
            if (ModelState.IsValid)
            {
                param.AffId = SessionService.UserSession.AffId;
                param.IsInternal = SessionService.IsInternal;

                var isValidMember = IsValidMember(param.UserName);

                if (isValidMember)
                {
                    var betlistSOAP = new BetlistSOAP
                    {
                        AffiliateName = AffiliateName,
                        Username = Regex.Replace(param.UserName, @"12[Bb][Ee][Tt]\$", string.Empty),
                        Product = "Sportsbook",
                        FromDate = param.Date,
                        ToDate = param.Date,
                        ExchangeRate = 1,
                        SupportedProducts = supportedProducts,
                        Language = SessionService.CurrentLanguage
                    };

                    using (var webService = new AffiliateAPISoapClient())
                    {
                        var viewModel = new MainBetListViewModel
                        {
                            BetListHtml = webService.GetBetlist(betlistSOAP),
                            SelectedDate = param.Date,
                            SelectedSportType = param.SportType,
                            UserName = param.UserName
                        };

                        return View(viewModel);
                    }
                }
            }

            return View(new MainBetListViewModel());
        }

        [HttpGet]
        [AjaxOnly]
        /// <summary>
        /// CodeReview:
        /// return string as viewmodel, dont need to use viewbag
        /// </summary>
        public ActionResult Result(BetListResultParam resultParam)
        {
            var isValidMember = IsValidMember($"12bet${resultParam.UserName}");
            if (isValidMember)
            {
                var criteria = new BetListResultsCriteria
                {
                    MatchId = resultParam.MatchId,
                    Bettype = resultParam.BetType,
                    RaceNo = resultParam.RaceNo,
                    SportType = resultParam.SportType,
                    IsOutRight = resultParam.IsOutRight,
                    UserName = resultParam.UserName,
                    BetId = resultParam.BetId,
                    Handicap2 = resultParam.Handicap2,
                    RefNo = resultParam.RefNo,
                    WinLossDate = resultParam.WinLossDate
                };

                using (var webService = new AffiliateAPISoapClient())
                {
                    ViewBag.Data = webService.GetBetlistResults(AffiliateName, Language, criteria);
                }
            }

            return View();
        }
        /// <summary>
        /// CodeReview:
        /// return string as viewmodel, dont need to use viewbag
        /// </summary>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ParlayDetail(BetListParlayDetailParam parlayDetailParam)
        {
            var criteria = new BetListSystemParlayDetailCriteria
            {
                TransId = parlayDetailParam.TransId,
                RefNo = parlayDetailParam.RefNo,
                BetId = parlayDetailParam.BetId,
                CustId = parlayDetailParam.CustId
            };

            using (var webService = new AffiliateAPISoapClient())
            {
                ViewBag.Data = webService.GetBetlistSystemParlay(AffiliateName, ExchangeRate, criteria, Language);
            }

            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult NewParlay(BetListNewParlayParam newParlayParam)
        {
            using (var webService = new AffiliateAPISoapClient())
            {
                var htmlResult = webService.GetNewBetlistParlay(
                    AffiliateName,
                    newParlayParam.TransId,
                    ExchangeRate,
                    newParlayParam.BetTeam,
                    newParlayParam.BetCheck,
                    newParlayParam.Handicap1,
                    newParlayParam.Handicap2,
                    SessionService.CurrentLanguage);

                return Content(htmlResult);
            }
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult CashOutDetail(BetListCashoutDetailParam cashoutDetailParam)
        {
            var criteria = new BetListDetailCriteria
            {
                TransId = cashoutDetailParam.TransId,
                RefNo = cashoutDetailParam.RefNo,
                BetType = cashoutDetailParam.BetType,
                BetdaqId = cashoutDetailParam.BetdaqId,
                ExchangeRate = 1
            };

            using (var webService = new AffiliateAPISoapClient())
            {
                return Content(webService.GetBetListCashOutDetail(criteria, cashoutDetailParam.IsCashOut, Language));
            }
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult OldParlay(string transId)
        {
            using (var webService = new AffiliateAPISoapClient())
            {
                return Content(webService.GetBetlistParlay(AffiliateName, transId, ExchangeRate, Language));
            }
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