using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels.Affiliate;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.AffiliateMenu)]
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
        [AllowByPassFunctionAuthorize]
        public ActionResult Index(ParamFromHome param, int groupId = 0)
        {
            var viewModel = new AffiliateFilterViewModel
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
            var viewModel = new AffiliateFilterViewModel
            {
                CommGroupList = _commissionGroupRepository.GetNormalCommGroupList(groupListParam),
                LanguageList = _languageService.GetInChangeMarkets(),
            };

            return new JsonNetResult(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UpdateUnauthorizedProperty]
        public ActionResult GetAffiliateList(GetAffiliateViewModel viewModel)
        {
            if (int.TryParse(viewModel.AffId, out int affId) || string.IsNullOrEmpty(viewModel.AffId))
            {
                viewModel.AffIdNumber = affId;
                viewModel.UserId = SessionService.UserId;
                var param = BuildGetModelToParam(viewModel);
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
        [FunctionAuthorize(FunctionName = Function.AffiliateInfo, IsHost = true)]
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
        [FunctionAuthorize(FunctionName = Function.AffPassword, IsHost = true)]
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

        private AffiliateInfoParam BuildGetModelToParam(GetAffiliateViewModel viewModel)
        {
            var supportLanguages = _languageService.GetInChangeMarkets();
            var queryLangs = LanguageHelper.BuildAllowQueryLanguages(viewModel.Language, supportLanguages);
            return new AffiliateInfoParam
            {
                AffId = viewModel.AffId,
                AffIdNumber = viewModel.AffIdNumber,
                Email = viewModel.Email,
                GroupId = viewModel.GroupId,
                IsExportExcel = viewModel.IsExportExcel,
                PageIndex = viewModel.PageIndex,
                PageSize = viewModel.PageSize,
                PreferenceLanguages = queryLangs,
                SortBy = viewModel.SortBy,
                SortDirectionType = viewModel.SortDirectionType,
                Status = viewModel.Status,
                UserId = viewModel.UserId,
                UserName = viewModel.UserName
            };
        }
    }
}
