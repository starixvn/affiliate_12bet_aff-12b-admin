namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
    using Fanex.Affiliate.Admin.Core.Extensions;
    using Fanex.Affiliate.Admin.Core.Services.Affiliate;
    using MetaModels.Operation;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Resources;
    using Services.Common;
    using Services.Operation;
    using ViewModels.Affiliate;

    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MarketingAccountController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IMarketingAccountService _marketingAccountService;
        private readonly ICommonDealService _commonDealService;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public MarketingAccountController(
            ISessionService sessionService,
            ISystemService systemService,
            IMarketingAccountService marketingAccountService,
            ICommonDealService commonDealService) : base(sessionService)
        {
            _systemService = systemService;
            _marketingAccountService = marketingAccountService;
            _commonDealService = commonDealService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/Affiliate"), FunctionName = Function.Affiliate },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest"), FunctionName = Function.Payment },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Portal/EmailHistory"), FunctionName = Function.EmailHistory },
                new SubMenu { Name = Lang.MarketingAccount, Url = Url.Content("~/Portal/MarketingAccount"), FunctionName = Function.Affiliate, IsActive = true }
            };

            var viewModel = new MarketingAccountViewModel
            {
                LanguageList = _systemService.GetLanguageList(),
                DealList = _commonDealService.GetNormalDealList(new GetNormalDealParam()).Where(c => c.GroupName.Contains("NoCommission-")),
                TargetCountryList = _systemService.GetTargetCountryList(),
                SubMenuList = subMenu
            };

            return View("~/Areas/Portal/Views/Affiliate/MarketingAccount.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetUserAccountInfo(int affId)
        {
            return new JsonNetResult(_jsonSettings)
            {
                Data = _marketingAccountService.GetMarketingAccountInfo(affId, Server)
            };
        }

        [FunctionAuthorize(FunctionName = Function.AddMarketingAccount, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public async Task<ActionResult> CreateMarketingAccount(CreateMarketingAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateModelStateErrResult(_jsonSettings);
            }

            AssignUserInfoParam(model);

            return new JsonNetResult(_jsonSettings)
            {
                Data = await _marketingAccountService.CreateMarketingAccountAsync(model, Server)
            };
        }

    }
}
