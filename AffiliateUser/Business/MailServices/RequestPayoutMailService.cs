using System;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

namespace Affiliate.MailServices
{
    public class RequestPayoutMailService : MailService
    {
        public string PreferredLanguage { get; set; }

        public string Action { get; set; }

        public string Date { get; set; }

        public object Amt { get; set; }

        public long TransId { get; set; }

        public DateTime DtmWinlostDate { get; set; }

        public void SendRequestPayoutMail()
        {
            if (string.IsNullOrEmpty(PreferredLanguage))
                PreferredLanguage = "en-US";

            string strRequestMail = "Email/PaymentRequest/PaymentRequest_" + PreferredLanguage + Common.EmailSuffix(AppSettings.Session.MailOption) + ".htm";
            string strCancelMail = "Email/PaymentCancel/PaymentCancel_" + PreferredLanguage + Common.EmailSuffix(AppSettings.Session.MailOption) + ".htm";

            string fileName;

            if (!string.IsNullOrEmpty(Action) && Action.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            {
                DtmWinlostDate = CastAide.AsDateTime(Date);
                fileName = strCancelMail;
            }
            else
            {
                fileName = strRequestMail;
            }

            string[] strTags = { "fname", "lname", "amount", "transid", "csdemail", "date", "rooturl", "lang" };
            string[] strValue = { AppSettings.Session.FirstName,
                AppSettings.Session.LastName,
                FormatHandler.FormatNumber2(Amt, "en-US"), TransId.ToString(),
                AppSettings.MailContact, DateFormat.ShortDateFormat(DtmWinlostDate),
                Common.CurrentRootAddress(), AppSettings.Session.PreferLang };
            string strContent = TemplateHandler.LoadHtml(fileName, strTags, strValue).Replace("\r\n", "");
            string strSubject = TemplateHandler.ReplaceBlock(ref strContent, "subject", "").Replace("\r\n", "").Trim();

            SendEmail(strContent, AppSettings.Session.Email, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
    }
}