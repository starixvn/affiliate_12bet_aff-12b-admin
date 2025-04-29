using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Resources;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class AppContactHelper
    {
        private const int MaxLengthInputString = 50;

        public static ResultInfo ValidateAppContactList(IEnumerable<ContactApp> contactAppList, string currentLanguage)
        {
            if (contactAppList == null || !contactAppList.Any())
            {
                return new ResultInfo 
                { 
                    IsOk = false, 
                    Message = Error.RequireIMAccount
                };
            }

            var firstItem = contactAppList.First();
            if (string.IsNullOrEmpty(firstItem.Value))
            {
                return new ResultInfo 
                { 
                    IsOk = false, 
                    Message = GetMessage(firstItem.Name, Error.IsRequired)
                };
            }

            var firstItemValidation = ValidateContactValue(firstItem, currentLanguage);
            if (!firstItemValidation.IsOk)
            {
                return firstItemValidation;
            }

            foreach (var contactApp in contactAppList.Skip(1))
            {
                if (!string.IsNullOrEmpty(contactApp.Value))
                {
                    var validationResult = ValidateContactValue(contactApp, currentLanguage);
                    if (!validationResult.IsOk)
                    {
                        return validationResult;
                    }
                }
            }

            return new ResultInfo { IsOk = true };
        }

        private static ResultInfo ValidateContactValue(ContactApp contactApp, string currentLanguage)
        {
            if (contactApp.Value.Length > MaxLengthInputString)
            {
                return new ResultInfo
                {
                    IsOk = false,
                    Message = GetMessage(contactApp.Name, Error.InvalidLength)
                };
            }

            if ((contactApp.Name == AppContact.Telegram || 
                 contactApp.Name == AppContact.Line || 
                 contactApp.Name == AppContact.WhatsApp) &&
                !RegexHelper.IsMatch(contactApp.Value, AppContact.GetRegexByAppAndLanguage(contactApp.Name, currentLanguage)))
            {
                return new ResultInfo 
                { 
                    IsOk = false, 
                    Message = GetMessage(contactApp.Name, Error.IsInvalid)
                };
            }

            return new ResultInfo { IsOk = true };
        }

        private static string GetMessage(string appName, string suffixMsg)
        {
            var name = appName.MatchWith("Skype") ? "Teams" : appName;

            return $"{name}{suffixMsg}";
        }
    }
} 