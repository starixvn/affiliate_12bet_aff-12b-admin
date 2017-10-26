using System;
using System.Data;
using System.Text;
using System.Web;
using DNAide.Core;
using DNAide.Web;
using Resources;

namespace GenerateHTML
{
    /// <summary>
    /// Generate content for Banner
    /// Written by : Tong Hoang Dung on 24/03/2009
    /// </summary>
    public class Banner
    {
        private static string Eu = "eneuro";
        private static string Es = "es";

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
                case "korean": return Resources.Lang.Korean;
                case "japanese": return Resources.Lang.Japanese;
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
                case "number game": return Resources.Lang.NumberGame;
                case "sportsbook": return Resources.Lang.Sportbook;
                case "racebook": return Resources.Lang.Racebook;
                case "sb bonus": return Resources.Lang.SBBonus;
                case "soccer": return Resources.Lang.Soccer;
                case "casino bonus": return Resources.Lang.CasinoBonus;
                case "baccarat": return Resources.Lang.Baccarat;
                case "blackjack": return Resources.Lang.Blackjack;
                case "slot": return Resources.Lang.Slot;
                case "roulette": return Resources.Lang.Roulette;
                case "sic bo": return Resources.Lang.Sicbo;

                default:
                    return value;
            }
        }

        public static string GetTextLink(string domain, string trackingcode, DataTable language, string target, string deviceType, string selectedLanguage = null)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            var textLinkList = new StringBuilder();
            string textLink = TemplateHandler.CopyBlock(htmlContent, "TextLinkList");
            string _12betLink = AppSettings.BetLink.ToString();

            switch (AppSettings.Session.PreferLang)
            {
                case "th-TH": _12betLink = AppSettings.BetLink_TH; break;
                case "zh-CN": _12betLink = AppSettings.BetLink_CS; break;
                case "zh-TW": _12betLink = AppSettings.BetLink_CS; break;
                case "vi-VN": _12betLink = AppSettings.BetLink_VI; break;
            }

            if (Common.IsAffInListEuLink())
                _12betLink = AppSettings.BetLink_EU;

            if (!string.IsNullOrEmpty(trackingcode))
            {
                trackingcode = "/" + trackingcode;
            }

            if (string.IsNullOrWhiteSpace(selectedLanguage))
            {
                foreach (DataRow dr in language.Rows)
                {
                    textLinkList.Append(GenLink(textLink, GetBannerLang(dr["value"].ToString()), _12betLink,
                        domain, trackingcode, Common.GetLangCode(dr["value"].ToString()), target, deviceType));
                }

                textLinkList.Append(GenLink(textLink, Lang.Default, _12betLink, domain, trackingcode, GenCodeByPreferLang(), target, deviceType));
            }
            else
            {
                textLinkList.Append(GenLink(textLink, GetBannerLang(selectedLanguage), _12betLink, domain, trackingcode, Common.GetLangCode(selectedLanguage), target, deviceType));
            }

            return textLinkList.ToString();
        }

        public static string GetBannerList(string category, bool isSearchByParentCateogry, string language,
            string imageType, string size, string currency, int pageIndex, int pagesize, ref int totalpage,
            string domain, string trackingcode, string target, int type, int afftype, string deviceType)
        {
            string tmpLanguage = language;// (language == "English (Europe)" ? "English" : language);//English Euro use English banner

            int nRows = 0;
            var result = (isSearchByParentCateogry)
                ? DataHandler.UserAff.SelAffBannerByParentCategory(category, tmpLanguage, imageType, size, currency, pageIndex, pagesize, type, afftype, deviceType)
                : DataHandler.UserAff.SelAffBanner(category, tmpLanguage, imageType, size, currency, pageIndex, pagesize, type, afftype, deviceType);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);

            int cols = 9;

            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                var contentList = new StringBuilder();
                int counter = 3;
                string signUpLink = AppSettings.BetLink.ToString();
                string strMedia = AppSettings.MediaJS;
                string src = AppSettings.MediaUrl;

                switch (AppSettings.Session.PreferLang)
                {
                    case "th-TH":
                        signUpLink = AppSettings.BetLink_TH.ToString();
                        strMedia = AppSettings.MediaJS_TH;
                        src = AppSettings.MediaUrlTH;
                        break;

                    case "zh-CN":
                    case "zh-TW":
                        signUpLink = AppSettings.BetLink_CS.ToString();
                        strMedia = AppSettings.MediaJS_CS;
                        src = AppSettings.MediaUrlCS;
                        break;

                    case "vi-VN":
                        signUpLink = AppSettings.BetLink_VI.ToString();
                        strMedia = AppSettings.MediaJS_VI;
                        src = AppSettings.MediaUrlVI;
                        break;
                }

                if (Common.IsAffInListEuLink())//20100910@Johnny: EU Link
                {
                    signUpLink = AppSettings.BetLink_EU;
                    strMedia = AppSettings.MediaJS_EU;
                    src = AppSettings.MediaUrlEU;
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
                    string rowText = content;
                    string aSignUpLink = signUpLink;
                    string tmpSrc = src + dr["filename"];
                    string[] strSize = dr["size"].ToString().Split(new char[] { 'x' });

                    counter++;
                    TemplateHandler.Assign(ref aSignUpLink, "{/lang}", GetLanguageUri(Common.GetLangCode(dr["language"].ToString())));
                    TemplateHandler.Assign(ref aSignUpLink, "{/DeviceType}", GetDeviceTypeUri(deviceType));
                    if (System.IO.Path.GetExtension(dr["filename"].ToString()) == ".swf")
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "ImageContent");
                        string flashContent = flContent;
                        TemplateHandler.Assign(ref rowText, "FlashContent_val", flashContent);
                        TemplateHandler.Assign(ref rowText, "custid_val", AppSettings.Session.CustID.ToString());
                        TemplateHandler.Assign(ref rowText, "srcnoswf_val", tmpSrc.Replace(".swf", string.Empty));
                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "FlashContent");
                        TemplateHandler.Assign(ref rowText, "12betlink_val", _12betLink);
                    }
                    TemplateHandler.Assign(ref rowText, "src_val", tmpSrc);
                    TemplateHandler.Assign(ref rowText, "signuplink_val", aSignUpLink);
                    TemplateHandler.Assign(ref rowText, "width_val", strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height_val", strSize[1]);
                    contentList.Append(rowText);
                }

                return contentList.ToString();
            }

            return Common.NoInfo(cols);
        }

        private static string GenLink(string textLink, string langText, string link, string domain, string trackingCode,
            string linkLang, string target, string deviceType)
        {
            TemplateHandler.Assign(ref textLink, "language_val", langText);
            TemplateHandler.Assign(ref link, "{domain}", domain);
            TemplateHandler.Assign(ref link, "{target}", target);
            TemplateHandler.Assign(ref link, "{custid}", AppSettings.Session.CustID.ToString());
            TemplateHandler.Assign(ref link, "{/TrackingCode}", trackingCode.Trim());
            TemplateHandler.Assign(ref link, "{/DeviceType}", GetDeviceTypeUri(deviceType));
            TemplateHandler.Assign(ref link, "{/lang}", GetLanguageUri(linkLang));

            TemplateHandler.Assign(ref textLink, "link_val", link);

            return textLink;
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

        private static string GenCodeByPreferLang()
        {
            switch (AppSettings.Session.PreferLang.ToLower())
            {
                case "en-us":
                    return "en";
                case "en-gb":
                case "en":
                    return "eneuro";
                case "zh-cn":
                    return "cs";
                case "zh-tw":
                    return "ch";
                case "th-th":
                    return "th";
                case "es-es":
                    return "es";
                case "vi-vn":
                    return "vn";
                case "id-id":
                    return "id";
                case "ko-kr":
                    return "ko";
                default:
                    return "en";
            }
        }
    }
}