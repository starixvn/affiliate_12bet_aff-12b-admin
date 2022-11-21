using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.DomainMenu)]
    public class DomainControlController : ReadOnlySessionBaseController
    {
        private readonly IDomainControlService _domainControlService;
        private readonly IDomainService _domainService;

        public DomainControlController(
            ISessionService sessionService,
            IDomainControlService domainControlService,
            IDomainService domainService) : base(sessionService)
        {
            _domainControlService = domainControlService;
            _domainService = domainService;
        }

        public ActionResult Index()
        {
            var model = new DomainViewModel
            {
                MarketList = _domainService.GetMarketList(new MarketParam { UserId = SessionService.UserId })
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlList(string marketId)
        {
            return new JsonNetResult
            {
                Data = _domainControlService.GetDomainControlList(marketId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlById(int domainControlId)
        {
            return new JsonNetResult
            {
                Data = _domainControlService.GetDomainControlById(domainControlId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult InsertDomainControl(InsertDomainControlParam param)
        {
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
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
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
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
                    Message = "The Domain Control is invalid!"
                }
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
