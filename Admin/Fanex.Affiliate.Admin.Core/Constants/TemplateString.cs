using Fanex.Affiliate.Admin.Core.DTO.Email;
using System;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class TemplateString
    {
        public const string CommonLayoutEmailTemplatePath = "~/Areas/Portal/Views/Shared/Email/CommonLayout.cshtml";

        public const string EmailTemplatePath = "~/Areas/Portal/Views/Shared/Email/{0}/Content.{1}.cshtml";

        public const string EmailImageRootPath = "{0}Areas/Portal/www/dist/imgs/email";

        public const string AffiliateDisapproved = "AffiliateDisapproved";

        public const string AffiliateApproved = "AffiliateApproved";

        public const string WithdrawalGuidance = "WithdrawalGuidance";

        public const string AffiliateInformation = "AffiliateInformation";

        public const string PaymentRequestApproved = "PaymentRequest/Approved";

        public const string PaymentRequestRejected = "PaymentRequest/Rejected";

        public const string ContactInfoHtml = "ContactInfo/Html";

        public const string ContactInfoText = "ContactInfo/Text";

        public static string GetContactInfoContent(EmailBase param, Func<string, string, object, string> getEmailTextContent)
        {
            if (param is null)
            {
                return string.Empty;
            }

            var contactInfoTemplateName = param.IsHtml ? ContactInfoHtml : ContactInfoText;
            return getEmailTextContent(contactInfoTemplateName, param.Language, param.ImageRootUrl);
        }
    }
}
