namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Attributes;
    using Constants;
    using DTO;
    using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
    using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
    using Fanex.Affiliate.Admin.Core.Services.UserPanel;
    using Fanex.Affiliate.Admin.Core.Utilities;
    using Newtonsoft.Json;
    using Resources;
    using Services.Common;
    using ViewModels.Affiliate;
    using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
    using Fanex.Affiliate.Admin.Core.Extensions;
    using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
    using Fanex.Affiliate.Admin.Core.Services.Affiliate;
    using Fanex.Affiliate.Admin.Core.Services.CommGroup;
    using Newtonsoft.Json.Serialization;

    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MarketingAccountController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IUserAccountService _userAccountService;
        private readonly IMarketingAccountService _marketingAccountService;
        private readonly ICommGroupCommonService _commGroupCommonService;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public MarketingAccountController(
            ISessionService sessionService,
            ISystemService systemService,
            IUserAccountService userAccountService,
            IMarketingAccountService marketingAccountService,
            ICommGroupCommonService commService) : base(sessionService)
        {
            _systemService = systemService;
            _userAccountService = userAccountService;
            _marketingAccountService = marketingAccountService;
            _commGroupCommonService = commService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/Affiliate"), FunctionName = Function.Affiliate },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest"), FunctionName = Function.Payment },
                new SubMenu { Name = Lang.RevenueGroup, Url = Url.Content("~/Portal/RevenueGroup"), FunctionName = Function.RevenueGroup },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Portal/EmailHistory"), FunctionName = Function.EmailHistory },
                new SubMenu { Name = Lang.MarketingAccount, Url = Url.Content("~/Portal/MarketingAccount"), FunctionName = Function.Affiliate, IsActive = true } // TODO: Need Review
            };

            var viewModel = new MarketingAccountViewModel
            {
                LanguageList = _systemService.GetLanguageList(),
                DealList = _commGroupCommonService.GetNormalCommGroupList(new GetGroupListParam()),
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
                Data = _marketingAccountService.GetMarketingAccountInfo(affId)
            };
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public async Task<ActionResult> CreateMarketingAccount(CreateMarketingAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateModelStateErrResult(_jsonSettings);
            }

            model.ContactAppList = AppContactHelper.HandleContactApps(model.PreferredLanguage);
            model.ContactApps = JsonConvert.SerializeObject(model.ContactAppList.Select(p => new ContactApp
            {
                Name = Server.HtmlEncode(p.Name),
                Value = Server.HtmlEncode(p.Value),
                Order = p.Order
            }));
            AssignUserInfoParam(model);

            return new JsonNetResult(_jsonSettings)
            {
                Data = await _marketingAccountService.CreateMarketingAccountAsync(model)
            };
        }
    }
}
