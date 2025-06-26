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

        public MarketingAccountService(IDynamicRepository dynamicRepository, IThirdPartyLicenseeService thirdPartyLicenseeService) : base(dynamicRepository)
        {
            _thirdPartyLicenseeService = thirdPartyLicenseeService;
        }

        public MarketingAccount GetMarketingAccountInfo(int affId)
        {
            var criteria = new UserAccountCriteria { AffId = affId };

            return GetTData<MarketingAccount>(criteria);
        }

        public async Task<ResultInfo> CreateMarketingAccountAsync(CreateMarketingAccountModel model, HttpServerUtilityBase server)
        {
            AssignContactApps(model, server);
            HtmlEncodeModelStringProperties(model, server);

            var command = model.CreateCommand();
            var result = await ExecuteReturnResultInfoAsync(command);
            var affInfo = CreateAffInfo(model, command.Username);

            await _thirdPartyLicenseeService.SendAffiliateInfo(affInfo);

            return result;
        }

        private static void HtmlEncodeModelStringProperties(CreateMarketingAccountModel model, HttpServerUtilityBase server)
        {
            var stringProperties = typeof(CreateMarketingAccountModel)
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

        private static void AssignContactApps(CreateMarketingAccountModel model, HttpServerUtilityBase server)
        {
            var contactApps = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(model.ContactApps);
            model.ContactAppList = AppContactHelper.HandleContactApps(model.PreferredLanguage, contactApps);
            model.ContactApps = JsonConvert.SerializeObject(model.ContactAppList.Select(p => new ContactApp
            {
                Name = server.HtmlEncode(p.Name),
                Value = server.HtmlEncode(p.Value),
                Order = p.Order

            }));
        }

        private static AffiliateBasicInfo CreateAffInfo(CreateMarketingAccountModel model, string userName)
        {
            return new AffiliateBasicInfo
            {
                AffId = model.AffId,
                AffCode = model.AffCode,
                ParentAffName = model.ParentAffName,
                GroupId = model.GroupId,
                AffName = model.UserName,
                AffType = model.AffType,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                Status = model.Status,
                MailOption = model.MailOption ?? false,
                PreferredLanguage = model.PreferredLanguage,
                PlayerUsername = userName,
                PlayerCustId = model.PlayerCustId,
                IsUsingPrivateCommGroup = model.IsPrivate
            };
        }
    }
}
