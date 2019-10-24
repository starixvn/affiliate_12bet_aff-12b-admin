using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Controllers.Domain
{
    [FunctionAuthorize(FunctionName = Function.Domain)]
    public class DomainController : BaseController
    {
        private readonly IDomainService _domainService;

        public DomainController(
            ISessionService sessionService,
            IDomainService domainService) : base(sessionService)
        {
            _domainService = domainService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateDomain, Url = Url.Content("~/Portal/Domain"), IsActive = true },
                new SubMenu { Name = Lang.Text12BetDomains, Url = Url.Content("~/Portal/_12BetDomains") },
                new SubMenu { Name = Lang.DomainControl, Url = Url.Content("~/Portal/DomainControl") }
            };

            return View(subMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetDomainList()
        {
            return new JsonNetResult
            {
                Data = _domainService.GetDomainList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult InsertDomain(InsertUpdateDomainParam param)
        {
            if (ModelState.IsValid)
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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Please input Domain's name."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
        public ActionResult UpdateDomain(InsertUpdateDomainParam param)
        {
            var isValidDomainId = param.DomainId >= Validation.MinId;
            if (ModelState.IsValid && isValidDomainId)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _domainService.UpdateDomain(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = !isValidDomainId ? "Domain Id is invalid." : "Please input Domain's name."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Media, IsHost = true)]
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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Domain id is incorrect."
                }
            };
        }

        private InsertUpdateDomainParam EncodeParam(InsertUpdateDomainParam param)
        {
            return new InsertUpdateDomainParam
            {
                DomainId = param.DomainId,
                DomainName = Server.HtmlEncode(param.DomainName),
                IsSSL = param.IsSSL
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
