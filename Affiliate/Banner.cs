using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Configuration;
using System.Web;
namespace GenerateHTML
{
    /// <summary>
    /// Generate content for Banner
    /// Written by : Tong Hoang Dung on 24/03/2009
    /// </summary>
    public class Banner
    {
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
        public static string GetTextLink(string trackingcode,DataTable language)        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            StringBuilder textLinkList = new StringBuilder();
            string textLink = TemplateHandler.CopyBlock(htmlContent, "TextLinkList");
            string _12betLink = AppSettings.BetLink.ToString();
            
            if (string.Compare(AppSettings.Session.PreferLang, "th-TH", true) == 0)
                _12betLink = AppSettings.BetLink_TH.ToString();
            else if (string.Compare(AppSettings.Session.PreferLang, "zh-CN", true) == 0 || string.Compare(AppSettings.Session.PreferLang, "zh-TW", true) == 0)
                _12betLink = AppSettings.BetLink_CS.ToString();

            TemplateHandler.Assign(ref _12betLink, "{custid}", AppSettings.Session.CustID.ToString());
            if (!string.IsNullOrEmpty(trackingcode))
            { 
                trackingcode = "/" + trackingcode;
            }
            TemplateHandler.Assign(ref _12betLink, "{/TrackingCode}",trackingcode.Trim());
            foreach (DataRow dr in language.Rows)
            {
                string aTextLink = textLink;
                string link = _12betLink;
                TemplateHandler.Assign(ref link, "lang", Common.GetLangCode(dr["value"].ToString()));
                TemplateHandler.Assign(ref aTextLink, "language_val", GetBannerLang(dr["value"].ToString()));
                TemplateHandler.Assign(ref aTextLink, "link_val", link);
                textLinkList.Append(aTextLink);
            }
            string defaultLink = _12betLink.Replace("/{lang}", string.Empty);
            string defaultTextLink = textLink;
            TemplateHandler.Assign(ref defaultTextLink, "language_val", Resources.Lang.Default);
            TemplateHandler.Assign(ref defaultTextLink, "link_val", defaultLink);

            textLinkList.Append(defaultTextLink);
            return textLinkList.ToString();
        }       
    }
}
