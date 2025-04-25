using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Resources;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class AppContactHelper
    {
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
                    Message = $"{firstItem.Name} is required" 
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
            if ((contactApp.Name == AppContact.Telegram || 
                 contactApp.Name == AppContact.Line || 
                 contactApp.Name == AppContact.WhatsApp) &&
                !Regex.IsMatch(contactApp.Value, AppContact.GetRegexByAppAndLanguage(contactApp.Name, currentLanguage)))
            {
                return new ResultInfo 
                { 
                    IsOk = false, 
                    Message = $"{contactApp.Name} is invalid" 
                };
            }

            return new ResultInfo { IsOk = true };
        }
    }
} 