using System;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Affiliate.Admin.Core.DTO;
    using System.Threading.Tasks;
    using System.Web;
    using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.UserAccount;
    using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
    using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
    using Fanex.Affiliate.Admin.Core.Services.ThirdPartyLicensee;
    using Fanex.Affiliate.Admin.Core.Utilities;
    using Fanex.Data.Repository;
    using Newtonsoft.Json;

    public class MarketingAccountService : BaseDynamicService, IMarketingAccountService
    {
        private readonly IThirdPartyLicenseeService _thirdPartyLicenseeService;
        private readonly IAffiliateService _affiliateService;
        private readonly ILicenseeService _licenseeService;
        private readonly IUserAccountService _userAccountService;
        private readonly IAppSetting _appSetting;
        
        private const int TEST_CURRENCY_ID = 20;

        public MarketingAccountService(
            IDynamicRepository dynamicRepository, 
            IThirdPartyLicenseeService thirdPartyLicenseeService,
            IAffiliateService affiliateService,
            ILicenseeService licenseeService,
            IUserAccountService userAccountService,
            IAppSetting appSetting
        ) : base(dynamicRepository)
        {
            _thirdPartyLicenseeService = thirdPartyLicenseeService;
            _affiliateService = affiliateService;
            _licenseeService = licenseeService;
            _userAccountService = userAccountService;
            _appSetting = appSetting;
        }

        public MarketingAccount GetMarketingAccountInfo(int affId, HttpServerUtilityBase server)
        {
            var criteria = new UserAccountCriteria { AffId = affId };
            var account = GetTData<MarketingAccount>(criteria);

            HtmlDecodeModelStringProperties(account, server, typeof(MarketingAccount));
            
            return account;
        }

        public async Task<ResultInfo> CreateMarketingAccountAsync(CreateMarketingAccountModel model, HttpServerUtilityBase server)
        {
            try
            {
                PrepareModelForCreation(model, server);

                var validationResult = ValidateCreateMarketingAccountModel(model);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult;
                }

                var command = model.CreateCommand();
                await ConfigurePlayerAccountIfNeeded(model, command);

                var result = await ExecuteCreateMarketingAccountCommand(command);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                await NotifyThirdPartyServices(model.AffId);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = Lang.UpdateIsFailed
                };
            }
        }

        private void PrepareModelForCreation(CreateMarketingAccountModel model, HttpServerUtilityBase server)
        {
            AssignContactApps(model, server);
            HtmlEncodeModelStringProperties(model, server, typeof(CreateMarketingAccountModel));
        }

        private ResultInfo ValidateCreateMarketingAccountModel(CreateMarketingAccountModel model)
        {
            if (!ValidatePaymentMethod(model.PreferredPayment))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = Lang.UpdateIsFailed
                };
            }

            return new ResultInfo(); // Default constructor should create a successful result
        }

        private async Task ConfigurePlayerAccountIfNeeded(CreateMarketingAccountModel model, dynamic command)
        {
            if (string.IsNullOrEmpty(command.Player_UserName))
            {
                command.Player_CustId = null;
                command.Player_Currency = null;
                return;
            }

            var currentAffInfo = _userAccountService.GetUserAccountInfo(model.AffId);
            var isPlayerAccountPreferred = PaymentMethod.PlayerAccount.Equals(model.PreferredPayment);
            var isDifferentUsername = currentAffInfo.PlayerUsername != model.PlayerUsername;

            if (isPlayerAccountPreferred && isDifferentUsername)
            {
                await SetPlayerAccountDetails(model, command);
            }
        }

        private async Task SetPlayerAccountDetails(CreateMarketingAccountModel model, dynamic command)
        {
            var memberSiteCust = await _licenseeService.GetMemberSiteCustomerInfo(model.PlayerUsername, _appSetting.MemberSiteId);
            var validationResult = _licenseeService.CheckCustomerIsValid(memberSiteCust);
            
            if (!validationResult.IsSuccessful)
            {
                throw new InvalidOperationException($"Customer validation failed: {validationResult.Message}");
            }

            command.Player_CustId = memberSiteCust.CustId;
            command.Player_Currency = DetermineCurrencyId(memberSiteCust);
        }

        private int DetermineCurrencyId(dynamic memberSiteCust)
        {
            return memberSiteCust.IsTest.GetValueOrDefault()
                ? TEST_CURRENCY_ID
                : memberSiteCust.CurrencyId.GetValueOrDefault();
        }

        private async Task<ResultInfo> ExecuteCreateMarketingAccountCommand(dynamic command)
        {
            return await ExecuteReturnResultInfoAsync(command);
        }

        private async Task NotifyThirdPartyServices(int affId)
        {
            var affInfo = _affiliateService.GetAffiliateBasicInfo(affId);
            affInfo.PlayerCustId = -1;
            await _thirdPartyLicenseeService.SendAffiliateInfo(affInfo);
        }

        private static void HtmlEncodeModelStringProperties(object model, HttpServerUtilityBase server, Type type)
        {
            var stringProperties = type
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var property in stringProperties)
            {
                var value = property.GetValue(model) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    property.SetValue(model, server.HtmlEncode(value));
                }
            }
        }

        private static void HtmlDecodeModelStringProperties(object model, HttpServerUtilityBase server, Type type)
        {
            var stringProperties = type
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var property in stringProperties)
            {
                var value = property.GetValue(model) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    property.SetValue(model, server.HtmlDecode(value));
                }
            }
        }

        private static void AssignContactApps(CreateMarketingAccountModel model, HttpServerUtilityBase server)
        {
            var contactApps = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(model.ContactApps);
            model.ContactAppList = AppContactHelper.HandleContactApps(model.PreferredLanguage, contactApps);
            model.ContactApps = JsonConvert.SerializeObject(model.ContactAppList.Select(p => new ContactApp
            {
                Name = server.HtmlDecode(p.Name),
                Value = server.HtmlDecode(p.Value),
                Order = p.Order
            }));
        }

        private static bool ValidatePaymentMethod(string preferredPayment)
        {
            return PaymentMethod.PlayerAccount.Equals(preferredPayment);
        }
    }
}
