using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
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
        private readonly IAffiliateService _affiliateService;

        private readonly ICommGroupCommonService _commGroupCommonService;

        private readonly ISystemService _systemService;

        public AffiliateController(
            ISessionService sessionService,
            IAffiliateService affiliateService,
            ICommGroupCommonService commGroupCommonService,
            ISystemService systemService) : base(sessionService)
        {
            _commGroupCommonService = commGroupCommonService;
            _affiliateService = affiliateService;
            _systemService = systemService;
        }

        [HttpGet]
        public ActionResult Index(ParamFromHome param, int groupId = 0)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/Affiliate"), FunctionName = Function.Affiliate, IsActive = true },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest"), FunctionName = Function.Payment },
                new SubMenu { Name = Lang.RevenueGroup, Url = Url.Content("~/Portal/RevenueGroup"), FunctionName = Function.RevenueGroup },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Portal/EmailHistory"), FunctionName = Function.EmailHistory },
                new SubMenu { Name = Lang.MarketingAccount, Url = Url.Content("~/Portal/MarketingAccount"), FunctionName = Function.Affiliate } // TODO: Need Review
            };

            var viewModel = new AffiliateInfoViewModel
            {
                CommGroupList = _commGroupCommonService.GetNormalCommGroupList(new GetGroupListParam()),
                LanguageList = _systemService.GetLanguageList(),
                TargetCountryList = _systemService.GetTargetCountryList(),
                SelectedGroupId = groupId,
                Param = param,
                SubMenuList = subMenu
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UnauthorizedPropertyCensor]
        public ActionResult GetAffiliateList(AffiliateInfoParam param)
        {
            if (int.TryParse(param.AffCode, out _) || string.IsNullOrEmpty(param.AffCode))
            {
                param.UserId = SessionService.UserId;
                var model = _affiliateService.GetAffiliateList(param);

                return GenerateJsonNetResult(model);
            }

            return GenerateJsonNetResult(new EntityResult<AffiliateInfo>
            {
                MainData = new List<AffiliateInfo>(),
                TotalItem = 0
            });
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        public ActionResult ToggleFavorite(FavoriteParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_affiliateService.ToggleFavorite(param));
            }

            return GenerateErrResult("Update Favorite failed!");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.EditAffDetails, IsHost = true)]
        public ActionResult ChangePassword(PasswordParam param)
        {
            if (ModelState.IsValid)
            {
                param.Password = CryptographyHelper.Encrypt(param.Password);
                AssignUserInfoParam(param);

                return GenerateJsonNetResult(_affiliateService.ChangePassword(param));
            }
            return GenerateErrResult("Change Password is failed!");
        }
    }
}
