using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Utilities;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerLeftPanelController : BaseController
    {
        private readonly ICustomerLeftPanelService _customerLeftPanelService;

        public CustomerLeftPanelController(
            ISessionService sessionService,
            ICustomerLeftPanelService customerLeftPanelService
            ) : base(sessionService)
        {
            _customerLeftPanelService = customerLeftPanelService;
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        [Compress]
        public ActionResult GetAffiliateInfo(string affUsername)
        {
            var accId = SessionService.UserId.ToString();
            var accOnId = SessionService.OnUserId.ToString();
            var sessionId = Session.SessionID;
            var affiliateInfo = _customerLeftPanelService.GetAffiliateInfoOnLeftPanel(affUsername, accId, accOnId, sessionId);

            return new JsonNetResult
            {
                Data = affiliateInfo
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        [Compress]
        public ActionResult GetOnlineList(string affUsername)
        {
            return new JsonNetResult
            {
                Data = _customerLeftPanelService.GetOnlineList(affUsername)
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult UpdateLanguage(UpdateLanguageParam param)
        {
            AssignParam(param);
            var test = _customerLeftPanelService.UpdateLanguage(param);
            return new JsonNetResult
            {
                Data = test
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult UpdateAffType(UpdateAffTypeParam param)
        {
            AssignParam(param);

            var test = _customerLeftPanelService.UpdateAffType(param);
            return new JsonNetResult
            {
                Data = test
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult UpdateStatus(UpdateStatusParam param)
        {
            AssignParam(param);
            var emailParam = new AffInformationEmail
            {
                RootUrl = SiteDomainHelper.GetRootUrl(this)
            };

            return new JsonNetResult
            {
                Data = _customerLeftPanelService.UpdateStatus(param, emailParam)
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            AssignParam(param);
            return new JsonNetResult
            {
                Data = _customerLeftPanelService.UpdateClosedStatus(param)
            };
        }

        private void AssignParam(AffiliateBasicInfoParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
