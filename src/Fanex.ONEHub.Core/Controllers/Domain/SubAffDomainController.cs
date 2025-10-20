using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class SubAffDomainController : ReadOnlySessionBaseController
    {
        private readonly ISubAffDomainService _subAffDomainService;

        public SubAffDomainController(
            ISessionService sessionService,
            ISubAffDomainService subAffDomainService) : base(sessionService)
        {
            _subAffDomainService = subAffDomainService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Domain/SubAffDomain.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetSubAffDomainList()
        {
            return new JsonNetResult
            {
                Data = _subAffDomainService.GetSubAffDomainList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult InsertSubAffDomain(InsertDomainParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                AssignUserInfoParam(encodeParam);

                return GenerateJsonNetResult(_subAffDomainService.InsertSubAffDomain(encodeParam));
            }

            return GenerateErrResult("Please input Sub-Affiliate Domain's name.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult UpdateSubAffDomain(UpdateDomainParam param)
        {
            var isValidDomainId = param.DomainId >= Validation.MinId;
            if (ModelState.IsValid && isValidDomainId)
            {
                var encodeParam = EncodeParam(param);
                var serializedParent = JsonConvert.SerializeObject(encodeParam);
                var newParam = JsonConvert.DeserializeObject<UpdateDomainParam>(serializedParent);
                newParam.DomainId = param.DomainId;
                AssignUserInfoParam(newParam);

                return GenerateJsonNetResult(_subAffDomainService.UpdateSubAffDomain(newParam));
            }

            return GenerateErrResult(isValidDomainId ? "Please input Sub-Affiliate Domain's name." : "Sub-Affiliate Domain Id is invalid.");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Domain, IsHost = true)]
        public ActionResult DeleteSubAffDomain(DeleteDomainParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_subAffDomainService.DeleteSubAffDomain(param));
            }

            return GenerateErrResult("Sub-Affiliate Domain Id is incorrect.");
        }

        private InsertDomainParam EncodeParam(InsertDomainParam param)
        {
            return new InsertDomainParam
            {
                DomainName = Server.HtmlEncode(param.DomainName),
                IsSSL = param.IsSSL,
                IsAffDomain = param.IsAffDomain
            };
        }
    }
}
