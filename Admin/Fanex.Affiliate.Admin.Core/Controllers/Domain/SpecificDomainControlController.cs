namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using DTO;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using Fanex.Affiliate.Admin.Core.Services.Domain;
    using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
    using Resources;
    using Services.Common;

    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class SpecificDomainControlController : ReadOnlySessionBaseController
    {
        private readonly ISpecificDomainControlService _domainControlService;

        public SpecificDomainControlController(
            ISessionService sessionService,
            ISpecificDomainControlService domainControlService) : base(sessionService)
        {
            _domainControlService = domainControlService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain?isAffDomain=true") },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/Domain?isAffDomain=false") },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl") },
                new SubMenu { Name = Lang.SubAffiliateDomain, Url = Url.Content("~/Portal/SubAffDomain") },
                new SubMenu { Name = Lang.SpecificDomainControl, Url = Url.Content("~/Portal/SpecificDomainControl"), IsActive = true }
            };

            var model = new DomainViewModel
            {
                SubMenuList = subMenu
            };

            return View("~/Areas/Portal/Views/Domain/SpecificDomainControl.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSpecificDomainControlList(SpecificDomainControlParam param)
        {
            var domainControlList = _domainControlService.GetSpecificDomainControl(param);

            return GenerateJsonNetResult(domainControlList);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSpecificDomainControlById(int specificDomainControlId)
        {
            var domainControl = _domainControlService.GetSpecificDomainControlById(specificDomainControlId);

            return GenerateJsonNetResult(domainControl);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult InsertSpecificDomainControl(InsertSpecificDomainControlParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.InsertSpecificDomainControl(param));
            }

            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult UpdateSpecificDomainControl(UpdateSpecificDomainControlParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.UpdateSpecificDomainControl(param));
            }

            return GenerateErrResult("The Specific Domain Control's info is invalid!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult DeleteSpecificDomainControl(DeleteDomainControlParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.DeleteSpecificDomainControl(param));
            }

            return GenerateErrResult("The Specific Domain Control is incorrect!");
        }
    }
}
