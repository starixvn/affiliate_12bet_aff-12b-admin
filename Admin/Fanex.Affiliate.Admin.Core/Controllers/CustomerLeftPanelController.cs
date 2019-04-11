using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.ViewModels.Customer;

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

            if ((user == null || user == string.Empty) && SessionService.AffId != 0)
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
    }
}
