namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
    using Fanex.Affiliate.Admin.Core.Services.Domain;
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
            return View("~/Areas/Portal/Views/Domain/SpecificDomainControl.cshtml");
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
                HandleEncodeParam(param);

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
                HandleEncodeParam(param);

                return GenerateJsonNetResult(_domainControlService.UpdateSpecificDomainControl(param));
            }

            return GenerateErrResult("The Specific Domain Control's info is invalid!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult DeleteSpecificDomainControl(DeleteSpecificDomainControlParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_domainControlService.DeleteSpecificDomainControl(param));
            }

            return GenerateErrResult("The Specific Domain Control is incorrect!");
        }

        private void HandleEncodeParam(InsertSpecificDomainControlParam param)
        {
            param.AffUsername = Server.HtmlEncode(param.AffUsername);
            param.AffDomain = Server.HtmlEncode(param.AffDomain);
            param.RedirectDomain = Server.HtmlEncode(param.RedirectDomain);
            AssignUserInfoParam(param);
        }
    }
}
