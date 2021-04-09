using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Customer;
using Fanex.Affiliate.Admin.Core.Utilities;
using Newtonsoft.Json;
using ONEbook.UM.Filters;
using System.Collections.Generic;
using System.Linq;
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
            _customerLeftPanelService = customerLeftPanelService;
            _sessionService = sessionService;
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

            var accOnId = _sessionService.OnUserId.ToString();
            var sessionId = Session.SessionID;
            var affiliateInfo = _customerLeftPanelService.GetAffiliateInfoOnLeftPanel(affUsername, accOnId, sessionId);

            if (!string.IsNullOrEmpty(affiliateInfo?.ContactApps))
            {
                affiliateInfo.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<CensoredContactApp>>(affiliateInfo.ContactApps).OrderBy(p => p.Order);
            }

            _sessionService.AffName = affUsername;

            return new JsonNetResult
            {
                Data = affiliateInfo
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
        [FunctionAuthorize(FunctionName = Function.LanguageAmendment, IsHost = true)]
        [HttpPost]
        public ActionResult UpdateLanguage(UpdateLanguageParam param)
        {
            if (ModelState.IsValid)
            {
                var updateLanguageParam = new UpdateLanguageParam
                {
                    AffId = param.AffId,
                    UserId = _sessionService.UserId,
                    Ip = Request.UserHostAddress,
                    PreferredLanguage = Server.HtmlEncode(param.PreferredLanguage)
                };

                return new JsonNetResult
                {
                    Data = _customerLeftPanelService.UpdateLanguage(updateLanguageParam)
                };
            }
            return new JsonResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update Language is failed!"
                }
            };
        }

        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.CustomerMenu, IsHost = true)]
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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update Affiliate Type is failed!"
                }
            };
        }

        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AffAccountStatus, IsHost = true)]
        [HttpPost]
        public ActionResult UpdateStatus(UpdateStatusParam param)
        {
            if (ModelState.IsValid)
            {
                var updateStatusParam = new UpdateStatusParam
                {
                    AffId = param.AffId,
                    UserId = _sessionService.UserId,
                    Ip = Request.UserHostAddress,
                    Status = Server.HtmlEncode(param.Status)
                };

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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update Status is failed!"
                }
            };
        }

        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AffAccountStatus, IsHost = true)]
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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update Closed Status is failed!"
                }
            };
        }

        private void AssignParam(AffiliateBasicInfoParam param)
        {
            param.UserId = _sessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
