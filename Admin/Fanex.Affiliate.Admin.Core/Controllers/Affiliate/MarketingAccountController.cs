using Fanex.Affiliate.Admin.Core.Extensions;
using Newtonsoft.Json.Serialization;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Web.Mvc;
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

    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MarketingAccountController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IUserAccountService _userAccountService;

        public MarketingAccountController(
            ISessionService sessionService,
            ISystemService systemService,
            IUserAccountService userAccountService) : base(sessionService)
        {
            _systemService = systemService;
            _userAccountService = userAccountService;
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
                TargetCountryList = _systemService.GetTargetCountryList(),
                SubMenuList = subMenu
            };

            return View("~/Areas/Portal/Views/Affiliate/MarketingAccount.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetUserAccountInfo(int affId)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return new JsonNetResult(options)
            {
                Data = _userAccountService.GetUserAccountInfo(affId)
            };
        }

        // TODO: Need Review
        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public async Task<ActionResult> CreateMarketingAccount(UserAccountParam param)
        {
            if (ModelState.IsValid)
            {
                var validateAppContacts = AppContactHelper.ValidateAppContactList(param.ContactAppList);
                if (!validateAppContacts.IsSuccessful)
                {
                    return GenerateJsonNetResult(validateAppContacts);
                }

                var userAccountParam = new UserAccountParam
                {
                    AffId = param.AffId,
                    FirstName = Server.HtmlEncode(param.FirstName),
                    LastName = Server.HtmlEncode(param.LastName),
                    Gender = param.Gender,
                    Email = Server.HtmlEncode(param.Email),
                    Phone = Server.HtmlEncode(param.Phone),
                    Address = Server.HtmlEncode(param.Address),
                    City = Server.HtmlEncode(param.City),
                    State = Server.HtmlEncode(param.State),
                    Postcode = Server.HtmlEncode(param.Postcode),
                    Country = Server.HtmlEncode(param.Country),
                    SiteUrl = Server.HtmlEncode(param.SiteUrl),
                    MoreSiteUrls = Server.HtmlEncode(param.MoreSiteUrls),
                    NoSiteDescription = Server.HtmlEncode(param.NoSiteDescription),
                    PreferredPayment = Server.HtmlEncode(param.PreferredPayment),
                    PlayerUsername = Server.HtmlEncode(param.PlayerUsername),
                    NetellerAccountId = Server.HtmlEncode(param.NetellerAccountId),
                    MoneybookersEmail = Server.HtmlEncode(param.MoneybookersEmail),
                    BankName = Server.HtmlEncode(param.BankName),
                    BankAddress = Server.HtmlEncode(param.BankAddress),
                    BankCode = Server.HtmlEncode(param.BankCode),
                    BankAccountName = Server.HtmlEncode(param.BankAccountName),
                    BankAccountNumber = Server.HtmlEncode(param.BankAccountNumber),
                    MailOption = param.MailOption,
                    HasWebSite = param.HasWebSite,
                    HasHostAffPersonalInfoPermission = SessionService.HasHostPermission(Function.AffPersonalInfo),
                };
                AssignUserInfoParam(userAccountParam);

                param.Player_CustId = -1; // Default value for Player_CustId
                if (param.ContactAppList != null)
                {
                    param.ContactAppList = AppContactHelper.HandleContactApps(param.ContactAppList, "en-US");
                    userAccountParam.ContactApps = JsonConvert.SerializeObject(param.ContactAppList.Select(p => new ContactApp
                    {
                        Name = Server.HtmlEncode(p.Name),
                        Value = Server.HtmlEncode(p.Value),
                        Order = p.Order
                    }));
                }

                return GenerateJsonNetResult(await _userAccountService.UpdateUserAccountAsync(userAccountParam));
            }

            return GenerateModelStateErrResult();
        }
    }
}
