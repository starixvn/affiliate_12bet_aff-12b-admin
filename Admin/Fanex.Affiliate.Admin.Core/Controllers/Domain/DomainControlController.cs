using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.Utilities.Domain;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.DomainMenu)]
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
            var model = new DomainViewModel
            {
                MarketList = GetVisibleMarketsByUser()
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlList(string marketId)
        {
            var visibleMarketIds = marketId;
            if (string.IsNullOrWhiteSpace(marketId))
            {
                visibleMarketIds = MarketHelper.GetVisibleMarketIds(marketId, GetVisibleMarketsByUser());
            }

            return new JsonNetResult
            {
                Data = _domainControlService.GetDomainControlList(visibleMarketIds)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlById(int domainControlId)
        {
            var domainControl = _domainControlService.GetDomainControlById(domainControlId);
            var isValidMarket = MarketHelper.IsValidMarketIds(domainControl?.MarketId, GetVisibleMarketsByUser());

            return new JsonNetResult
            {
                Data = isValidMarket ? domainControl : null
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult InsertDomainControl(InsertDomainControlParam param)
        {
            if (ModelState.IsValid && MarketHelper.IsValidMarketIds(param.MarketId, GetVisibleMarketsByUser()))
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _domainControlService.InsertDomainControl(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "The Domain Control's info is invalid!"
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult UpdateDomainControl(UpdateDomainControlParam param)
        {
            if (ModelState.IsValid && MarketHelper.IsValidMarketIds(param.MarketId, GetVisibleMarketsByUser()))
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _domainControlService.UpdateDomainControl(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "The Domain Control's info is invalid!"
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult DeleteDomainControl(DeleteDomainControlParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _domainControlService.DeleteDomainControl(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "The Domain Control is incorrect!"
                }
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }

        private IEnumerable<Market> GetVisibleMarketsByUser()
        {
            return _domainControlService.GetVisibleMarketsByUser(SessionService.UserId);
        }
    }
}
