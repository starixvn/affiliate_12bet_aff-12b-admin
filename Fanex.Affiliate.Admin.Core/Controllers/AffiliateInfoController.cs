using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Attributes;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class AffiliateInfoController : BaseController
    {
        private readonly IAffiliateInfoRepository _affiliateInfoRepository;

        private readonly ICommissionGroupRepository _commissionGroupRepository;

        private readonly ILanguageService _languageService;

        public AffiliateInfoController(
            ISessionService sessionService,
            IAffiliateInfoRepository affiliateInfoRepository,
            ICommissionGroupRepository commissionGroupRepository,
            ILanguageService languageService) : base(sessionService)
        {
            _commissionGroupRepository = commissionGroupRepository;
            _affiliateInfoRepository = affiliateInfoRepository;
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var param = new GetGroupListParam { AffId = -1 };
            var viewModel = new AffiliateInfoViewModel
            {
                CommGroupList = _commissionGroupRepository.GetNormalCommGroupList(param),
                LanguageList = _languageService.GetLanguageList()
            };

            return View("~/Areas/Portal/Views/AffiliateInfo/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAffiliateList(AffiliateInfoParam param)
        {
            param.UserId = SessionService.UserId;
            var model = _affiliateInfoRepository.GetAffiliateList(param);
            return Json(model);
        }
    }
}