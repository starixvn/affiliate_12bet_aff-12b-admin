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

        public static ResultInfo ValidateAppContactList(IEnumerable<ContactApp> contactAppList)
        {
            if (contactAppList == null || !contactAppList.Any())
            {
                return ResultInfo.GenerateErr("At least one IM account is required");
            }

            var firstItem = contactAppList.First();
            if (string.IsNullOrEmpty(firstItem.Value))
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
