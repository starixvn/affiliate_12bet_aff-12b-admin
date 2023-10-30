using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.DomainMenu)]
    public class DomainControlController : ReadOnlySessionBaseController
    {
        private readonly IDomainControlService _domainControlService;
        private readonly ITargetCountryService _targetCountryService;

        public DomainControlController(
            ISessionService sessionService,
            IDomainControlService domainControlService,
            ITargetCountryService targetCountryService) : base(sessionService)
        {
            _domainControlService = domainControlService;
            _targetCountryService = targetCountryService;
        }

        public ActionResult Index()
        {
            var model = new DomainViewModel
            {
                TargetCountryList = GetVisibleTargetCountriesByUser()
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlList(string targetCountryCodes)
        {
            var visibleTargetCountryCodes = SystemHelper.GetVisibleTargetCountryCodes(targetCountryCodes, GetVisibleTargetCountriesByUser());

            return new JsonNetResult
            {
                Data = _domainControlService.GetDomainControlList(visibleTargetCountryCodes)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainControlById(int domainControlId)
        {
            var domainControl = _domainControlService.GetDomainControlById(domainControlId);
            var isValidTargetCountry = SystemHelper.IsValidTargetCountryCodes(domainControl?.TargetCountryCode, GetVisibleTargetCountriesByUser());

            return new JsonNetResult
            {
                Data = isValidTargetCountry ? domainControl : null
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult InsertDomainControl(InsertDomainControlParam param)
        {
            if (ModelState.IsValid && SystemHelper.IsValidTargetCountryCodes(param.TargetCountryCode, GetVisibleTargetCountriesByUser()))
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _domainControlService.InsertDomainControl(param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("The Domain Control's info is invalid!")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult UpdateDomainControl(UpdateDomainControlParam param)
        {
            if (ModelState.IsValid && SystemHelper.IsValidTargetCountryCodes(param.TargetCountryCode, GetVisibleTargetCountriesByUser()))
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _domainControlService.UpdateDomainControl(param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("The Domain Control's info is invalid!")
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
                Data = ResultInfo.GenerateErr("The Domain Control is incorrect!")
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }

        private IEnumerable<TargetCountry> GetVisibleTargetCountriesByUser()
        {
            return _targetCountryService.GetVisibleTargetCountriesByUser(SessionService.UserId);
        }
    }
}
