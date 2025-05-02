namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    using Attributes;
    using Constants;
    using Extensions;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using Fanex.Affiliate.Admin.Core.Services.Domain;
    using Fanex.Affiliate.Admin.Core.Utilities;
    using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
    using Services.Common;
    using System.Web.Mvc;

    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainController : ReadOnlySessionBaseController
    {
        private readonly IDomainService _domainService;
        private readonly ISystemService _systemService;

        public DomainController(
            ISessionService sessionService,
            IDomainService domainService,
            ISystemService systemService) : base(sessionService)
        {
            _domainService = domainService;
            _systemService = systemService;
        }

        public virtual ActionResult Index()
        {
            return RedirectToCommonIndex();
        }

        protected ActionResult RedirectToCommonIndex(bool IsAffDomain = true)
        {
            var model = new DomainViewModel
            {
                IsAffDomain = IsAffDomain
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
        public ActionResult GetTargetCountryListByDomain(int affDomainId = 0, int redirectDomainId = 0)
        {
            return new JsonNetResult
            {
                Data = _domainService.GetTargetCountryListByDomain(affDomainId, redirectDomainId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult InsertDomain(InsertDomainParam param)
        {
            if (ModelState.IsValid && TargetCountryHelper.IsValidTargetCountryCodes(param.BlockTargetCountry, _systemService.GetTargetCountryList()))
            {
                HandleEncodeParam(param);

                return GenerateJsonNetResult(_domainService.InsertDomain(param));
            }

            return GenerateErrResult("The Domain' info is invalid!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult UpdateDomain(UpdateDomainParam param)
        {
            if (ModelState.IsValid && TargetCountryHelper.IsValidTargetCountryCodes(param.BlockTargetCountry, _systemService.GetTargetCountryList()))
            {
                HandleEncodeParam(param);

                return GenerateJsonNetResult(_domainService.UpdateDomain(param));
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

        private void HandleEncodeParam(InsertDomainParam param)
        {
            param.DomainName = Server.HtmlEncode(param.DomainName);
            param.BlockTargetCountry = Server.HtmlEncode(param.BlockTargetCountry);
            AssignUserInfoParam(param);
        }
    }
}
