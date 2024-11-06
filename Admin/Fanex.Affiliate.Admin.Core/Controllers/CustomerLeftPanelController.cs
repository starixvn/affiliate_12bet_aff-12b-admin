using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Customer;
using Fanex.Affiliate.Admin.Core.Utilities;
using ONEbook.UM.AspNetMVC.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
    public class CustomerLeftPanelController : Controller
    {
        private readonly ICustomerLeftPanelService _customerLeftPanelService;
        private readonly ISessionService _sessionService;

        public CustomerLeftPanelController(
            ISessionService sessionService,
            ICustomerLeftPanelService customerLeftPanelService
            )
        {
            _sessionService = sessionService;
            _customerLeftPanelService = customerLeftPanelService;
        }

        [ValidateAntiForgeryHeader]
        [UpdateUnauthorizedProperty]
        [HttpPost]
        public ActionResult GetAffiliateInfo(string affUsername)
        {
            if (string.IsNullOrEmpty(affUsername))
            {
                return new JsonResult();
            }

            _sessionService.AffName = affUsername;
            var affInfo = _customerLeftPanelService.GetAffiliateInfoOnLeftPanel(affUsername, _sessionService.OnUserId.ToString(), Session.SessionID);

            return new JsonNetResult
            {
                Data = affInfo
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult GetOnlineList(string affUsername)
        {
            if (string.IsNullOrEmpty(affUsername))
            {
                return new JsonResult();
            }

            return new JsonNetResult
            {
                Data = _customerLeftPanelService.GetOnlineList(affUsername)
            };
        }

        [ValidateAntiForgeryHeader]
        [MultiFunctionAuthorize(FunctionName = Function.LanguageAmendment, FunctionNameOption = Function.ManagePendingAff, IsHost = true)]
        [HttpPost]
        public ActionResult UpdateLanguage(UpdateLanguageParam param)
        {
            if (ModelState.IsValid)
            {
                var updateLanguageParam = new UpdateLanguageParam
                {
                    AffId = param.AffId,
                    PreferredLanguage = Server.HtmlEncode(param.PreferredLanguage)
                };
                AssignParam(param);

                return new JsonNetResult
                {
                    Data = _customerLeftPanelService.UpdateLanguage(updateLanguageParam)
                };
            }
            return new JsonResult
            {
                Data = ResultInfo.GenerateErr("Update Language is failed!")
            };
        }

        [ValidateAntiForgeryHeader]
        [MultiFunctionAuthorize(FunctionName = Function.AffiliateInfo, FunctionNameOption = Function.ManagePendingAff, IsHost = true)]
        [HttpPost]
        public ActionResult UpdateAffType(UpdateAffTypeParam param)
        {
            if (ModelState.IsValid)
            {
                AssignParam(param);
                return new JsonNetResult
                {
                    Data = _customerLeftPanelService.UpdateAffType(param)
                };
            }
            return new JsonResult
            {
                Data = ResultInfo.GenerateErr("Update Affiliate Type is failed!")
            };
        }

        [ValidateAntiForgeryHeader]
        [MultiFunctionAuthorize(FunctionName = Function.AffiliateInfo, FunctionNameOption = Function.ManagePendingAff, IsHost = true)]
        [HttpPost]
        public ActionResult UpdateStatus(UpdateStatusParam param)
        {
            if (ModelState.IsValid)
            {
                var updateStatusParam = new UpdateStatusParam
                {
                    AffId = param.AffId,
                    Status = Server.HtmlEncode(param.Status)
                };
                AssignParam(updateStatusParam);

                var emailParam = new AffInformationEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this)
                };

                return new JsonNetResult
                {
                    Data = _customerLeftPanelService.UpdateStatus(updateStatusParam, emailParam)
                };
            }
            return new JsonResult
            {
                Data = ResultInfo.GenerateErr("Update Status is failed!")
            };
        }

        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AffiliateInfo, IsHost = true)]
        [HttpPost]
        public ActionResult UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            if (ModelState.IsValid)
            {
                AssignParam(param);
                return new JsonNetResult
                {
                    Data = _customerLeftPanelService.UpdateClosedStatus(param)
                };
            }
            return new JsonResult
            {
                Data = ResultInfo.GenerateErr("Update Closed Status is failed!")
            };
        }

        private void AssignParam(AffiliateBasicInfoParam param)
        {
            param.UserId = _sessionService.UserId;
            param.Ip = Request.GetClientIP();
        }
    }
}
