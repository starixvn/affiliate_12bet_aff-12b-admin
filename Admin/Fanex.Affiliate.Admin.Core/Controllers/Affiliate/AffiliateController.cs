using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels.Affiliate;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class AffiliateController : ReadOnlySessionBaseController
    {
        private readonly IAffiliateInfoRepository _affiliateInfoRepository;

        private readonly ICommissionGroupRepository _commissionGroupRepository;

        private readonly ILanguageService _languageService;

        public AffiliateController(
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
        public ActionResult Index(ParamFromHome param, int groupId = 0)
        {
            var viewModel = new AffiliateInfoViewModel
            {
                SelectedGroupId = groupId,
                Param = param
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFilter()
        {
            var groupListParam = new GetGroupListParam { AffId = -1 };
            var viewModel = new AffiliateInfoViewModel
            {
                CommGroupList = _commissionGroupRepository.GetNormalCommGroupList(groupListParam),
                LanguageList = _languageService.GetLanguageList(),
            };

            return new JsonNetResult(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAffiliateList(AffiliateInfoParam param)
        {
            int affId;
            if (int.TryParse(param.AffId, out affId) || string.IsNullOrEmpty(param.AffId))
            {
                param.AffIdNumber = affId;
                param.UserId = SessionService.UserId;
                var model = _affiliateInfoRepository.GetAffiliateList(param);

                return new JsonNetResult
                {
                    Data = model
                };
            }

            return new JsonNetResult
            {
                Data = new EntityResult<AffiliateInfo>
                {
                    MainData = new List<AffiliateInfo>(),
                    TotalItem = 0
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ToggleFavorite(FavoriteParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return Json(_affiliateInfoRepository.ToggleFavorite(param));
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = "Update Favorite failed!"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ChangePassword(PasswordParam param)
        {
            if (ModelState.IsValid)
            {
                param.Password = CryptographyHelper.Encrypt(param.Password);
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return Json(_affiliateInfoRepository.ChangePassword(param));
            }
            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = "Change Password is failed!"
            });
        }
    }
}
