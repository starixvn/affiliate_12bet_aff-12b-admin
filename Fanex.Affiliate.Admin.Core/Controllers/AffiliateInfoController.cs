using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
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
        public ActionResult Index(int groupId = 0)
        {
            var param = new GetGroupListParam { AffId = -1 };
            var viewModel = new AffiliateInfoViewModel
            {
                CommGroupList = _commissionGroupRepository.GetNormalCommGroupList(param),
                LanguageList = _languageService.GetLanguageList(),
                SelectedGroupId = groupId
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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ToggleFavorite(FavoriteParam param)
        {
            if (param.AffId == 0)
            {
                return Json(new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update Favorite failed!"
                });
            }

            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;

            return Json(_affiliateInfoRepository.ToggleFavorite(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ChangePassword(PasswordParam param)
        {
            if (param.AffId != 0 && ModelState.IsValid)
            {
                param.Password = CryptographyHelper.Encrypt(param.Password);
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return Json(_affiliateInfoRepository.ChangePassword(param));
            }
            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = "Change Password failed!"
            });
        }
    }
}