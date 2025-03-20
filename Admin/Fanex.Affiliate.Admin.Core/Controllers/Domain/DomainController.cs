namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using Enum;
    using Extensions;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using Fanex.Affiliate.Admin.Core.Resources;
    using Fanex.Affiliate.Admin.Core.Services.Domain;
    using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
    using Newtonsoft.Json;
    using Services.Common;
    using Utilities.Market;

    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainController : ReadOnlySessionBaseController
    {
        private readonly IDomainService _domainService;

        public DomainController(
            ISessionService sessionService,
            IDomainService domainService) : base(sessionService)
        {
            _domainService = domainService;
        }

        public ActionResult Index(bool isAffDomain = true)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain?isAffDomain=true"), IsActive = isAffDomain },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/Domain?isAffDomain=false"), IsActive = !isAffDomain },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl") },
                new SubMenu { Name = Lang.SubAffiliateDomain, Url = Url.Content("~/Portal/SubAffDomain") }
            };

            var model = new DomainViewModel
            {
                SubMenuList = subMenu,
                IsAffDomain = isAffDomain
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainList(bool isAffDomain)
        {
            return GenerateJsonNetResult(_domainService.GetDomainList(isAffDomain));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainById(int domainId, bool isAffDomain = true)
        {
            return new JsonNetResult
            {
                Data = _domainService.GetDomainById(domainId, isAffDomain)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketList(int affDomainId = 0, int redirectDomainId = 0)
        {
            return new JsonNetResult
            {
                Data = _domainService.GetMarketListByDomain(affDomainId, redirectDomainId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult InsertDomain(InsertDomainParam param)
        {
            if (ModelState.IsValid && MarketHelper.IsValidMarketIds(param.BlockMarkets, GetVisibleMarketsNotUsing()))
            {
                var encodeParam = EncodeParam(param);
                AssignUserInfoParam(encodeParam);

                return GenerateJsonNetResult(_domainService.InsertDomain(encodeParam));
            }

            return GenerateErrResult("Please input Domain's name.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult UpdateDomain(UpdateDomainParam param)
        {
            if (ModelState.IsValid && MarketHelper.IsValidMarketIds(param.BlockMarkets, GetVisibleMarketsNotUsing()))
            {
                var encodeParam = EncodeParam(param);
                var serializedParent = JsonConvert.SerializeObject(encodeParam);
                var newParam = JsonConvert.DeserializeObject<UpdateDomainParam>(serializedParent);
                newParam.DomainId = param.DomainId;
                AssignUserInfoParam(newParam);

                return GenerateJsonNetResult(_domainService.UpdateDomain(newParam));
            }

            return GenerateErrResult("The Domain' info is invalid!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult DeleteDomain(DeleteDomainParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainService.DeleteDomain(param));
            }

            return GenerateErrResult("The Domain is incorrect!");
        }

        private InsertDomainParam EncodeParam(InsertDomainParam param)
        {
            return new InsertDomainParam
            {
                DomainName = Server.HtmlEncode(param.DomainName),
                BlockMarkets = Server.HtmlEncode(param.BlockMarkets),
                IsSSL = param.IsSSL,
                IsAffDomain = param.IsAffDomain
            };
        }

        private IEnumerable<Market> GetVisibleMarketsNotUsing()
        {
            return _domainService.GetVisibleMarketNotInStatus((byte)MarketStatus.Using);
        }
    }
}
