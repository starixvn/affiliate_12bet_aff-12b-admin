namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    using Attributes;
    using Constants;
    using DTO;
    using Extensions;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using Fanex.Affiliate.Admin.Core.Services.Domain;
    using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
    using Resources;
    using Services.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Utilities;
    using Utilities.Market;

    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainControlController : ReadOnlySessionBaseController
    {
        private readonly IDomainControlService _domainControlService;

        public DomainControlController(
            ISessionService sessionService,
            IDomainControlService domainControlService) : base(sessionService)
        {
            _domainControlService = domainControlService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain?isAffDomain=true") },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/Domain?isAffDomain=false") },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl"), IsActive = true },
                new SubMenu { Name = Lang.SubAffiliateDomain, Url = Url.Content("~/Portal/SubAffDomain") },
                new SubMenu { Name = Lang.SpecificDomainControl, Url = Url.Content("~/Portal/SpecificDomainControl") }
            };

            var model = new DomainControlViewModel
            {
                SubMenuList = subMenu,
                AffSubDomainList = _domainControlService.GetSubAffDomainControlList(),
                MarketList = GetVisibleMarkets()
            };

            return View("~/Areas/Portal/Views/Domain/DomainControl.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSubAffDomainControlList()
        {
            return GenerateJsonNetResult(_domainControlService.GetSubAffDomainControlList());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlList(string market, string subDomainRegex, bool isGetAll = true)
        {
            var domainControlList = _domainControlService.GetDomainControl(market);

            if (isGetAll || string.IsNullOrEmpty(subDomainRegex))
            {
                return GenerateJsonNetResult(domainControlList);
            }

            var regexPattern = $"^({subDomainRegex})\\.|^({subDomainRegex})$";
            return GenerateJsonNetResult(domainControlList.Where(d => RegexHelper.IsMatchIgnoreCase(d.AffDomain, regexPattern)));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlById(int domainControlId)
        {
            var domainControl = _domainControlService.GetDomainControlById(domainControlId);
            var isValidMarket = MarketHelper.IsValidMarketIds(domainControl?.MarketId, GetVisibleMarkets());

            return new JsonNetResult
            {
                Data = isValidMarket ? domainControl : null
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult InsertDomainControl(InsertDomainControlParam param)
        {
            if (ModelState.IsValid && MarketHelper.IsValidMarketIds(param.MarketId, GetVisibleMarkets()))
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.InsertDomainControl(param));
            }

            return GenerateErrResult("The Domain Control's info is invalid!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult UpdateDomainControl(UpdateDomainControlParam param)
        {
            if (ModelState.IsValid && MarketHelper.IsValidMarketIds(param.MarketId, GetVisibleMarkets()))
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.UpdateDomainControl(param));
            }

            return GenerateErrResult("The Domain Control's info is invalid!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult DeleteDomainControl(DeleteDomainControlParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.DeleteDomainControl(param));
            }

            return GenerateErrResult("The Domain Control is incorrect!");
        }

        private IEnumerable<Market> GetVisibleMarkets()
        {
            return _domainControlService.GetMarketListByDomain();
        }
    }
}
