using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.ViewModels.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerLeftPanelController : BaseController
    {
        private readonly ICustomerLeftPanelRepository _customerLeftPanelRepository;
        private readonly IAffiliateService _affiliateService;
        private readonly ILanguageService _languageService;

        public CustomerLeftPanelController(
            ISessionService sessionService,
            ICustomerLeftPanelRepository customerLeftPanelRepository,
            IAffiliateService affiliateService,
            ILanguageService languageService) : base(sessionService)
        {
            _customerLeftPanelRepository = customerLeftPanelRepository;
            _affiliateService = affiliateService;
            _languageService = languageService;
        }

        public ActionResult Index(string user)
        {
            var model = new LeftPanelViewModel
            {
                AffName = user,
                LanguageList = _languageService.GetLanguageList()
            };

            if (string.IsNullOrEmpty(user) && SessionService.AffId != 0)
            {
                var affInfo = _affiliateService.GetAffiliateBasicInfo(SessionService.AffId);
                model.AffName = affInfo.UserName;
            }

            return View("~/Areas/Portal/Views/Customer/LeftPanel.cshtml", model);
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        [Compress]
        public ActionResult GetAffiliateInfo(string affUsername)
        {
            return new JsonNetResult
            {
                Data = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUsername)
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        [Compress]
        public ActionResult GetOnlineList(string affUsername)
        {
            return new JsonNetResult
            {
                Data = _customerLeftPanelRepository.GetOnlineList(affUsername)
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult UpdateLanguage(UpdateLanguageParam param)
        {
            AssignParam(param);
            var test = _customerLeftPanelRepository.UpdateLanguage(param);
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
            param.Language = LanguageSystem.DefaultLanguage;

            var test = _customerLeftPanelRepository.UpdateAffType(param);
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
            return new JsonNetResult
            {
                Data = _customerLeftPanelRepository.UpdateStatus(param)
            };
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            AssignParam(param);
            return new JsonNetResult
            {
                Data = _customerLeftPanelRepository.UpdateClosedStatus(param)
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
