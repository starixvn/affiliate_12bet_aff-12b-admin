using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class AppContactHelper
    {
        private const int MaxLengthInputString = 50;
        private const int OneContactOrder = 1;
        private const int SecondContactOrder = 2;
        private const int ThirdContactOrder = 3;

        public static ResultInfo ValidateAppContactList(IEnumerable<ContactApp> contactAppList, bool isMarketingAccount = false)
        {
            if (!isMarketingAccount && (contactAppList == null || !contactAppList.Any()))
            {
                return ResultInfo.GenerateErr("At least one IM account is required");
            }

            var firstItem = contactAppList.First();
            if (!isMarketingAccount && string.IsNullOrEmpty(firstItem.Value))
            {
                return ResultInfo.GenerateErr(GetMessage(firstItem.Name, "is required"));
            }

            var firstItemValidation = ValidateContactValue(firstItem);
            if (!firstItemValidation.IsSuccessful)
            {
                return firstItemValidation;
            }

            foreach (var contactApp in contactAppList.Skip(1))
            {
                if (!string.IsNullOrEmpty(contactApp.Value))
                {
                    var validationResult = ValidateContactValue(contactApp);
                    if (!validationResult.IsSuccessful)
                    {
                        return validationResult;
                    }
                }
            }

            return ResultInfo.Success;
        }

        public static IEnumerable<ContactApp> HandleContactApps(string languageCode, IEnumerable<ContactApp> contactApps = null)
        {
            var result = new ContactApp[3];

            AppContact.FirsAppContactList.TryGetValue(languageCode, out var firstContactName);
            AppContact.SecondAppContactList.TryGetValue(languageCode, out var secondContactName);
            AppContact.ThirdAppContactList.TryGetValue(languageCode, out var thirdContactName);


            var contactAppDict = contactApps != null
                ? contactApps.ToDictionary(c => c.Name, c => c)
                : new Dictionary<string, ContactApp>();

            if (!string.IsNullOrEmpty(firstContactName))
            {
                result[OneContactOrder - 1] = contactAppDict.TryGetValue(firstContactName, out var contact)
                    ? contact
                    : new ContactApp { Name = firstContactName, Value = string.Empty, Order = OneContactOrder };
            }

            if (!string.IsNullOrEmpty(secondContactName))
            {
                result[SecondContactOrder - 1] = contactAppDict.TryGetValue(secondContactName, out var contact)
                    ? contact
                    : new ContactApp { Name = secondContactName, Value = string.Empty, Order = SecondContactOrder };
            }

            if (!string.IsNullOrEmpty(thirdContactName))
            {
                result[ThirdContactOrder - 1] = contactAppDict.TryGetValue(thirdContactName, out var contact)
                    ? contact
                    : new ContactApp { Name = thirdContactName, Value = string.Empty, Order = ThirdContactOrder };
            }

            return result.Where(c => c != null);
        }

        private static ResultInfo ValidateContactValue(ContactApp contactApp)
        {
            if (contactApp.Value.Length > MaxLengthInputString)
            {
                return ResultInfo.GenerateErr(GetMessage(contactApp.Name, "no more than 50 characters"));
            }

            if ((contactApp.Name == AppContact.Telegram ||
                 contactApp.Name == AppContact.Line ||
                 contactApp.Name == AppContact.WhatsApp) &&
                !RegexHelper.IsMatch(contactApp.Value, AppContact.GetRegexByApp(contactApp.Name)))
            {
                return ResultInfo.GenerateErr(GetMessage(contactApp.Name, "is invalid"));
            }

            return ResultInfo.Success;
        }

        private static string GetMessage(string appName, string suffixMsg)
        {
            var name = appName.MatchWith("Skype") ? "Teams" : appName;

            return $"{name} {suffixMsg}";
        }
    }
}
