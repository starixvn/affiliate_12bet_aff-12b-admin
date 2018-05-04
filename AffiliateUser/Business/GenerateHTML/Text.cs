using System;
using System.Data;
using System.Text;
using System.Web;
using AffiliateUser.Helpers;
using DNAide.Core;
using DNAide.Web;

/// <summary>
/// Summary description for Text
/// </summary>
namespace GenerateHTML
{
    public class Text
    {
        public Text()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        static string Eu = "eneuro";
        static string Es = "es";
        public static string GetBannerLang(string value)
        {

            switch (value.ToLower())
            {
                case "bonus": return Resources.Lang.Bonus;
                case "tennis": return Resources.Lang.Tennis;
                case "football": return Resources.Lang.Football;
                case "poker": return Resources.Lang.Poker;
                case "image": return Resources.Lang.Image;
                case "flash": return Resources.Lang.Flash;
                case "english": return Resources.Lang.English;
                case "chinese traditional": return Resources.Lang.ChineseTraditio;
                case "chinese simplified": return Resources.Lang.ChineseSimplifi;
                case "thai": return Resources.Lang.Thai;
                case "spanish": return Resources.Lang.Spanish;
                case "indonesian": return Resources.Lang.Indonesian;
                case "vietnamese": return Resources.Lang.Vietnamese;
                case "english (europe)": return Resources.Lang.EnglishEU;
                case "casino": return Resources.Lang.Casino;
                case "basketball": return Resources.Lang.Basketball;
                case "tennins": return Resources.Lang.Tennis;
                case "rmb": return Resources.Lang.RMB;
                case "myr": return Resources.Lang.MYR;
                case "thb": return Resources.Lang.THB;
                case "usd": return Resources.Lang.USD;
                case "eur": return Resources.Lang.EUR;
                case "gbp": return Resources.Lang.GBP;
                case "aud": return Resources.Lang.AUD;
                case "chf": return Resources.Lang.CHF;

                default:
                    return value;

            }

        }

        public static string GetTextList(string category, string language, string currency, int pageIndex, int pagesize, ref int totalpage,
            DataRow selectedDomain, string trackingcode, string target, string deviceType)
        {
            var result = DataHandler.UserAff.SelAffText(category, language, currency, pageIndex, pagesize, deviceType);
            int nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int cols = 9;

            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("UserInfo/MediaText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 3;
                string signUpLink = AppSettings.BetLink.ToString();
                string strMedia = AppSettings.MediaJS;

                switch (AppSettings.Session.PreferLang)
                {
                    case "th-TH":
                        strMedia = AppSettings.MediaJS_TH;
                        break;
                    case "zh-CN":
                    case "zh-TW":
                        strMedia = AppSettings.MediaJS_CS;
                        break;
                    case "vi-VN":
                        strMedia = AppSettings.MediaJS_CS;
                        break;
                }

                if (Common.IsAffInListEuLink())
                {
                    strMedia = AppSettings.MediaJS_EU;
                }

                var selectedDomainName = selectedDomain["DomainName"].ToString();
                var scheme = CastAide.AsBoolean(selectedDomain["IsSSL"], false) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                target = TrackingCodeHelper.Convert(target);
                TemplateHandler.Assign(ref signUpLink, "{scheme}", scheme);
                TemplateHandler.Assign(ref signUpLink, "{domain}", selectedDomainName);
                TemplateHandler.Assign(ref signUpLink, "{target}", target);

                if (language == "English (Europe)")
                    TemplateHandler.Assign(ref signUpLink, "lang", Eu);
                TemplateHandler.Assign(ref signUpLink, "{custid}", AppSettings.Session.CustID.ToString());

                if (!string.IsNullOrEmpty(trackingcode))
                {
                    trackingcode = "/" + trackingcode;
                }
                TemplateHandler.Assign(ref signUpLink, "{/TrackingCode}", trackingcode.Trim());
                string _12betLink = selectedDomainName;

                if (_12betLink.IndexOf("/") != -1)
                {
                    _12betLink = _12betLink.Substring(0, _12betLink.IndexOf("/"));
                }


                string flContent = HttpContext.Current.Server.HtmlEncode(TemplateHandler.LoadTemplate("UserInfo/FlashCode.htm"));
                TemplateHandler.Assign(ref flContent, "src", strMedia);

                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    string aSignUpLink = signUpLink;
                    TemplateHandler.Assign(ref aSignUpLink, "{/lang}", GetLanguageUri(Common.GetLangCode(dr["language"].ToString())));
                    TemplateHandler.Assign(ref aSignUpLink, "target", target);

                    TemplateHandler.Assign(ref rowText, "messagevalue", Common.FormatStringHtml(dr["message"].ToString()));
                    TemplateHandler.Assign(ref rowText, "signuplink_val", aSignUpLink);
                    TemplateHandler.Assign(ref rowText, "12betlink_val", _12betLink);
                    TemplateHandler.Assign(ref rowText, "textid", dr["BannerID"]);
                    TemplateHandler.Assign(ref rowText, "bannerurl", AppSettings.BannerUrl);
                    contentList.Append(rowText);
                }

                return contentList.ToString();
            }

            return Common.NoInfo(cols);
        }

        private static string GetLanguageUri(string linkLang)
        {
            if (string.IsNullOrEmpty(linkLang))
            {
                return string.Empty;
            }

            return "/" + linkLang;
        }
    }
}
