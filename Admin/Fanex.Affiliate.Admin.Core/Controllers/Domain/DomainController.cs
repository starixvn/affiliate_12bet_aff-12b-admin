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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.DomainMenu)]
    public class DomainController : ReadOnlySessionBaseController
    {
        protected readonly IDomainService _domainService;
        private readonly ITargetCountryService _targetCountryService;

        public DomainController(
            ISessionService sessionService,
            IDomainService domainService,
            ITargetCountryService targetCountryService) : base(sessionService)
        {
            _domainService = domainService;
            _targetCountryService = targetCountryService;
        }

        public virtual ActionResult Index()
        {
            var model = new DomainViewModel
            {
                IsAffDomain = true
            };

            return View("~/Areas/Portal/Views/Domain/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainList(bool isAffDomain = true)
        {
            return new JsonNetResult
            {
                Data = _domainService.GetDomainList(SessionService.UserId, isAffDomain)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDomainById(int domainId, bool isAffDomain = true)
        {
            return new JsonNetResult
            {
                Data = _domainService.GetDomainById(SessionService.UserId, domainId, isAffDomain)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTargetCountryList(TargetCountryParam param)
        {
            param.UserId = SessionService.UserId;

            return new JsonNetResult
            {
                Data = _domainService.GetTargetCountryList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult InsertDomain(InsertDomainParam param)
        {
            if (ModelState.IsValid && SystemHelper.IsValidTargetCountryCodes(param.ExcludedTargetCountries, GetVisibleTargetCountriesNotUsingByUser()))
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _domainService.InsertDomain(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("The Domain's info is invalid!")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult UpdateDomain(UpdateDomainParam param)
        {
            if (ModelState.IsValid && SystemHelper.IsValidTargetCountryCodes(param.ExcludedTargetCountries, GetVisibleTargetCountriesNotUsingByUser()))
            {
                var encodeParam = EncodeParam(param);
                var serializedParent = JsonConvert.SerializeObject(encodeParam);
                UpdateDomainParam newParam = JsonConvert.DeserializeObject<UpdateDomainParam>(serializedParent);
                newParam.DomainId = param.DomainId;
                GetUserInfoParam(newParam);

                return new JsonNetResult
                {
                    Data = _domainService.UpdateDomain(newParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("The Domain's info is invalid!")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DomainMenu, IsHost = true)]
        public ActionResult DeleteDomain(DeleteDomainParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _domainService.DeleteDomain(param)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("The Domain is incorrect!")
            };
        }

        private InsertDomainParam EncodeParam(InsertDomainParam param)
        {
            return new InsertDomainParam
            {
                DomainName = Server.HtmlEncode(param.DomainName),
                ExcludedTargetCountries = Server.HtmlEncode(param.ExcludedTargetCountries),
                IsSSL = param.IsSSL,
                IsAffDomain = param.IsAffDomain
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }

        private IEnumerable<TargetCountry> GetVisibleTargetCountriesNotUsingByUser()
        {
            return _targetCountryService.GetVisibleTargetCountriesNotUsingByUser(SessionService.UserId);
        }
    }
}
