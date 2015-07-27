using System;
using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Configuration;
using System.Web;

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

        public static string GetTextLink(string trackingcode, DataTable language, string target)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            StringBuilder textLinkList = new StringBuilder();
            string textLink = TemplateHandler.CopyBlock(htmlContent, "TextLinkList");
            string _12betLink = AppSettings.BetLink.ToString();

            switch (AppSettings.Session.PreferLang)
            {
                case "th-TH": _12betLink = AppSettings.BetLink_TH.ToString(); break;
                case "zh-CN": _12betLink = AppSettings.BetLink_CS.ToString(); break;
                case "zh-TW": _12betLink = AppSettings.BetLink_CS.ToString(); break;
                case "vi-VN": _12betLink = AppSettings.BetLink_VI.ToString(); break;
            }

            if (Common.IsAffInListEuLink())
                _12betLink = AppSettings.BetLink_EU;

            if (!string.IsNullOrEmpty(trackingcode))
            {
                trackingcode = "/" + trackingcode;
            }

            foreach (DataRow dr in language.Rows)
            {
                textLinkList.Append(GenLink(textLink, GetBannerLang(dr["value"].ToString()), _12betLink,
                    trackingcode, Common.GetLangCode(dr["value"].ToString()), target));
            }

            //Default link
            textLinkList.Append(GenLink(textLink, Resources.Lang.Default, _12betLink, trackingcode, GenCodeByPreferLang(), target));
            //}           
            return textLinkList.ToString();
        }

        public static string GetTextList(string category, string language, string currency, int pageIndex, int pagesize, ref int totalpage,
            string domain, string trackingcode, string target, string deviceType)
        {
            DbResult result = null;
            result = DataHandler.UserAff.SelAffText(category, language, currency, pageIndex, pagesize, deviceType);
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
                        signUpLink = AppSettings.BetLink_TH.ToString();
                        strMedia = AppSettings.MediaJS_TH;
                        break;
                    case "zh-CN":
                        signUpLink = AppSettings.BetLink_CS.ToString();
                        strMedia = AppSettings.MediaJS_CS;
                        break;
                    case "zh-TW":
                        signUpLink = AppSettings.BetLink_CS.ToString();
                        strMedia = AppSettings.MediaJS_CS;
                        break;
                    case "vi-VN":
                        signUpLink = AppSettings.BetLink_VI.ToString();
                        strMedia = AppSettings.MediaJS_CS;
                        break;
                }

                if (Common.IsAffInListEuLink())
                {
                    signUpLink = AppSettings.BetLink_EU;
                    strMedia = AppSettings.MediaJS_EU;
                }

                TemplateHandler.Assign(ref signUpLink, "{domain}", domain);
                TemplateHandler.Assign(ref signUpLink, "{target}", target);

                if (language == "English (Europe)")
                    TemplateHandler.Assign(ref signUpLink, "lang", Eu);
                TemplateHandler.Assign(ref signUpLink, "{custid}", AppSettings.Session.CustID.ToString());

                if (!string.IsNullOrEmpty(trackingcode))
                {
                    trackingcode = "/" + trackingcode;
                }
                TemplateHandler.Assign(ref signUpLink, "{/TrackingCode}", trackingcode.Trim());
                string _12betLink = signUpLink;
                if (_12betLink.IndexOf("http://") >= 0)
                {
                    _12betLink = _12betLink.Substring(7);
                }

                if (_12betLink.IndexOf("/") > 0)
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
                    TemplateHandler.Assign(ref aSignUpLink, "{/DeviceType}", GetDeviceTypeUri(deviceType));
                    TemplateHandler.Assign(ref aSignUpLink, "target", target);

                    TemplateHandler.Assign(ref rowText, "messagevalue", Common.FormatStringHtml (dr["message"].ToString()));
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

        static string GenLink(string textLink, string langText, string link, string trackingCode, string linkLang, string target)
        {
            TemplateHandler.Assign(ref textLink, "language_val", langText);
            TemplateHandler.Assign(ref link, "{target}", target);
            TemplateHandler.Assign(ref link, "{custid}", AppSettings.Session.CustID.ToString());
            TemplateHandler.Assign(ref link, "{/TrackingCode}", trackingCode.Trim());
            if (string.IsNullOrEmpty(linkLang))
                link = link.Replace("/{lang}", string.Empty);
            else
                TemplateHandler.Assign(ref link, "lang", linkLang);

            TemplateHandler.Assign(ref textLink, "link_val", link);
            return textLink;
        }

        static string GenCodeByPreferLang()
        {
            switch (AppSettings.Session.PreferLang.ToLower())
            {
                case "en-us":
                    return "en";
                case "en":
                    return "eneuro";
                case "zh-cn":
                    return "cs";
                case "zh-tw":
                    return "ct";
                case "th-th":
                    return "th";
                case "es-es":
                    return "es";
                case "vi-vn":
                    return "vi";
                case "id-id":
                    return "id";
                default:
                    return "en";
            }
        }

        private static string GetLanguageUri(string linkLang)
        {
            if (string.IsNullOrEmpty(linkLang))
            {
                return string.Empty;
            }

            return "/" + linkLang;
        }

        private static string GetDeviceTypeUri(string deviceType)
        {
            if (string.IsNullOrEmpty(deviceType) || !deviceType.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return "/mobile";
        }
    }
}
