namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using Extensions;
    using Fanex.Affiliate.Admin.Core.DTO.Common;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using Fanex.Affiliate.Admin.Core.Services.Domain;
    using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
    using MetaModels.CommissionGroup;
    using Services.CommGroup;
    using Services.Common;
    using Utilities;

    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainControlController : ReadOnlySessionBaseController
    {
        private readonly IDomainControlService _domainControlService;
        private readonly ICommGroupCommonService _commGroupCommonService;

        public DomainControlController(
            ISessionService sessionService,
            IDomainControlService domainControlService,
            ICommGroupCommonService commGroupCommonService) : base(sessionService)
        {
            _domainControlService = domainControlService;
            _commGroupCommonService = commGroupCommonService;
        }

        public ActionResult Index()
        {
            var model = new DomainControlViewModel
            {
                AffSubDomainList = _domainControlService.GetSubAffDomainControlList(),
                TargetCountryList = GetVisibleTargetCountries(),
                DealList = _commGroupCommonService.GetNormalCommGroupList(new GetGroupListParam())
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
        public ActionResult GetDomainControlList(string targetCountryCode, string subDomainRegex, bool isGetAll = true)
        {
            var domainControlList = _domainControlService.GetDomainControl(targetCountryCode);

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
            var isValidMarket = TargetCountryHelper.IsValidTargetCountryCodes(domainControl?.TargetCountryCode, GetVisibleTargetCountries());

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
            if (ModelState.IsValid && TargetCountryHelper.IsValidTargetCountryCodes(param.TargetCountryCode, GetVisibleTargetCountries()))
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
            if (ModelState.IsValid && TargetCountryHelper.IsValidTargetCountryCodes(param.TargetCountryCode, GetVisibleTargetCountries()))
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

        private IEnumerable<TargetCountry> GetVisibleTargetCountries()
        {
            return _domainControlService.GetTargetCountryListByDomain();
        }
    }
}
