using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class AffiliateInfoController : BaseController
    {
        private readonly IAffiliateInfoRepository _affiliateInfoRepository;

        private readonly ILanguageService _languageService;

        public AffiliateInfoController(
            ISessionService sessionService,
            IAffiliateInfoRepository affiliateInfoRepository,
            ILanguageService languageService) : base(sessionService)
        {
            _affiliateInfoRepository = affiliateInfoRepository;
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new AffiliateInfoViewModel
            {
                CommGroupList = _languageService.GetLanguageList(),
                LanguageList = _languageService.GetLanguageList()
            };

            return View("~/Areas/Portal/Views/AffiliateInfo/Index.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetAffiliateList(AffiliateInfoParam param)
        {
            return Json(_affiliateInfoRepository.GetAffiliateList(param));
        }
    }
}